using System.Windows;
using System.Windows.Input;

namespace CMusicPlayer.UI.General
{
    /// <summary>
    ///     Interaction logic for SelectionControl.xaml
    /// </summary>
    internal partial class SelectionControl
    {
        public static readonly RoutedEvent ToArtistsEvent =
            EventManager.RegisterRoutedEvent("ToArtists", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(SelectionControl));


        public static readonly RoutedEvent ToAlbumsEvent =
            EventManager.RegisterRoutedEvent("ToAlbums", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(SelectionControl));


        public static readonly RoutedEvent ToTracksEvent =
            EventManager.RegisterRoutedEvent("ToTracks", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(SelectionControl));


        public static readonly RoutedEvent ToGenresEvent =
            EventManager.RegisterRoutedEvent("ToGenres", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(SelectionControl));

        public SelectionControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event RoutedEventHandler ToArtists
        {
            add => AddHandler(ToAlbumsEvent, value);
            remove => RemoveHandler(ToAlbumsEvent, value);
        }

        public event RoutedEventHandler ToAlbums
        {
            add => AddHandler(ToAlbumsEvent, value);
            remove => AddHandler(ToAlbumsEvent, value);
        }

        public event RoutedEventHandler ToTracks
        {
            add => AddHandler(ToAlbumsEvent, value);
            remove => RemoveHandler(ToAlbumsEvent, value);
        }

        public event RoutedEventHandler ToGenres
        {
            add => AddHandler(ToAlbumsEvent, value);
            remove => RemoveHandler(ToAlbumsEvent, value);
        }

        private void ToTracksView(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ToTracksEvent));
        }

        private void ToAlbumsView(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ToAlbumsEvent));
        }

        private void ToArtistsView(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ToArtistsEvent));
        }

        private void ToGenresView(object sender, MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ToGenresEvent));
        }
    }
}