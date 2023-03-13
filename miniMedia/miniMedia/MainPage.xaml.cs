using Android.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.OS.Storage;
using Android.App;
using Android;
//using Android.Support.V4.App;
namespace miniMedia
{
    public partial class MainPage : ContentPage
    {
        MediaPlayer player = new MediaPlayer();
        List<string> subscribedList = new List<string>();
        List<string> subscribedTitle = new List<string>();
        public string[] musicFile = new string[] {};
        public string[] musicMix = new string[] { "problemSkrable.mp3", "problemMakijaz.mp3", "problemFlary.mp3", "problemGround.mp3", "problemVhs.mp3", "problem258.mp3", "problemRotterdam.mp3" };
        public string[] musicMix2 = new string[] { "gymKesha.mp3","gymJustdance.mp3", "gymSiewjezdza.mp3", "gymInmymind.mp3", "gymLoversonthesun.mp3", "gymPartofme.mp3", "gymSweetbutapsycho.mp3" };
        public string[] musicTitle = new string[] {};
        public string[] mixTitle = new string[] { "PRO8L3M - Skrable ", "PRO8L3M - Makijaż", "PRO8L3M - Flary", "PRO8L3M - Ground Zero", "PRO8L3M - VHS", "PRO8L3M - 258", "PRO8L3M - Rotterdam"};
        public string[] mixTitle2 = new string[] { "Ke$ha - TiK ToK","Lady Gaga - Just Dance", "Włodar - Się wjeżdża", "Dynoro & Gigi D’Agostino - In My Mind", "David Guetta - Lovers On The Sun", "Katy Perry - Part Of Me", "Ava Max - Sweet but Psycho" };
        
        public int musicIndex = 0;
        public MainPage()
        {
            InitializeComponent();
           
        }
        public void testing()
        {
            //subscribedList.Add(musicFile[musicIndex]);
            player.PlaybackParams.SetSpeed(2.0f);   
        }
        public void subscribeCheck()
        {
            for (int i = 0; i < subscribedList.Count; i++)
            {
                if (musicFile[musicIndex] != subscribedList[i])
                {
                    subscribeBtn.IsEnabled = true;
                }
                else if(musicFile[musicIndex] == subscribedList[i]){
                    subscribeBtn.IsEnabled = false;
                    break;
                }
            }
        }
        public void turnOnBtn()
        {
            var fD = global::Android.App.Application.Context.Assets.OpenFd(musicFile[musicIndex]);
            player.SetDataSource(fD.FileDescriptor, fD.StartOffset, fD.Length);
            player.Prepare();
            previousBtn.IsEnabled = true;
            playBtn.IsEnabled = true;
            nextBtn.IsEnabled = true;
            subscribeBtn.IsEnabled = true;
            playBtn.Text = "▶️";
            lblTitle.Text = "";
            
        }
        public void changeMusic()
        {
            player.Reset();
            var fD = global::Android.App.Application.Context.Assets.OpenFd(musicFile[musicIndex]);
            player.SetDataSource(fD.FileDescriptor, fD.StartOffset, fD.Length);
            player.Prepare();
            player.Start();
            lblTitle.Text = musicTitle[musicIndex];
            playBtn.Text = "⏸";
        }
        private void playBtn_Clicked(object sender, EventArgs e)
        {
            if(player.IsPlaying == true)
            {
                player.Pause();
                playBtn.Text = "▶️";   
            }
            else
            {
                player.Start();
                playBtn.Text = "⏸";
                lblTitle.Text = musicTitle[musicIndex];
                subscribeCheck();
            }        
        }
        private void volumeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var volume = Convert.ToSingle(volumeSlider.Value);
            player.SetVolume(volume, volume);
        }
        private void timeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var value = player.Duration;
            player.SeekTo((int)(value*timeSlider.Value));       
        }
        private void previousBtn_Clicked(object sender, EventArgs e)
        {
            if (musicIndex-1<0)
            {
                musicIndex = musicFile.Length-1;
            }
            else
            {
                musicIndex--;
            }
            changeMusic();
            subscribeCheck();
        }
        private void nextBtn_Clicked(object sender, EventArgs e)
        {
            if(musicFile.Length <= musicIndex + 1)
            {
                musicIndex = 0;
            }
            else
            {
                musicIndex++;
                timeSlider.Value = 0;
            }
            changeMusic();
            subscribeCheck();
        }
        private void selectMix_Clicked(object sender, EventArgs e)
        {
            if (player.IsPlaying != true)
            {
                player.Reset();
                lblTitle.Text = "";
                musicIndex = 0; 
                musicFile = musicMix;
                musicTitle = mixTitle;
                turnOnBtn();
            }
        }
        private void selectMix2_Clicked(object sender, EventArgs e)
        {
            if(player.IsPlaying != true) { 

                player.Reset();
                lblTitle.Text = "";
                musicIndex = 0;
                musicFile = musicMix2; 
                musicTitle = mixTitle2;
                turnOnBtn();
            }
        }
        private void subscribeBtn_Clicked(object sender, EventArgs e)
        {
            subscribedList.Add(musicFile[musicIndex]);
            subscribedTitle.Add(musicTitle[musicIndex]);
            subscribeBtn.IsEnabled = false;
        }
        private void selectSubscribed_Clicked(object sender, EventArgs e)
        {
            if (player.IsPlaying != true)
            {
                if(subscribedList.Count == 0)
                {
                }
                else
                {
                    player.Reset();
                    musicIndex = 0;
                    musicFile = subscribedList.ToArray();
                    musicTitle = subscribedTitle.ToArray();
                    lblTitle.Text = musicTitle[musicIndex];
                    turnOnBtn();
                }
            }
             
        }
    }
}
