using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Media.Models;
using File = TagLib.File;

namespace CMusicPlayer.Data.Files
{
    internal class FileManager
    {
        private static readonly string[] SupportedFormats =
        {
            "aa", "aax", "aac", "aiff", "ape", "dsf", "flac", "m4a", "m4b", "m4p", "mp3", "mpc", "mpp", "ogg", "oga",
            "wav", "wma", "wv", "webm"
        };

        private readonly IDatabase db;

        public FileManager(IDatabase db)
        {
            this.db = db;
        }

        public event EventHandler FilesUploaded;

        public async Task AddLocalFiles(Action<string>? setNotificationMessage = null)
        {
            var files = OpenFileDialog();
            await AddFilesToDatabase(files, setNotificationMessage);
            setNotificationMessage?.Invoke(string.Empty);
            FilesUploaded?.Invoke(this, EventArgs.Empty);
        }

        public async Task AddLocalFolder(Action<string>? setNotificationMessage = null)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var dirInfo = new DirectoryInfo(dialog.SelectedPath);
            var files = dirInfo.GetFiles("*.*", SearchOption.AllDirectories)
                .Select(file => file.FullName);
            await AddFilesToDatabase(files, setNotificationMessage);
            setNotificationMessage?.Invoke(string.Empty);
            FilesUploaded?.Invoke(this, EventArgs.Empty);
        }

        public async Task AddFilesToDatabase(IEnumerable<string> filepaths, Action<string>? cb = null)
        {
            await Task.Run(async () => // Perform On Another Thread To Prevent Freezing UI
            {
                await Task.WhenAll(filepaths.Select(async file => // Allows Loop To Run In Parallel
                {
                    cb?.Invoke($"Processing: {file}");
                    if (!SupportedFormats.Any(file.EndsWith)) return; // Checks File Extension Is Supported
                    try
                    {
                        // Taglib api
                        await db.SaveTrack(TrackModel.FromFile(File.Create(file)));
                        // ATL api
//                        await db.SaveTrack(Track.FromFile(file));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }));
            });
        }

        /// <summary>
        ///     Opens Files Dialog And Returns Array Of File Paths
        /// </summary>
        private static IEnumerable<string> OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog {Multiselect = true};
            fileDialog.ShowDialog();
            return fileDialog.FileNames;
        }
    }
}