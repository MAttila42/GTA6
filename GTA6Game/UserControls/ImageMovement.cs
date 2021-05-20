using GTA6Game.Pages;
using GTA6Game.Routing;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace GTA6Game.UserControls
{
    public class ImageMovement
    {

        public void Move(Image bg, double pos1, double pos2, double pos3, int loopCount, List<string> képekNeve, RoutingHelper router)
        {

            TranslateTransform animatedTranslateTransform = new TranslateTransform();
            bg.RenderTransform = animatedTranslateTransform;

            DoubleAnimationUsingKeyFrames translationAnimation = new DoubleAnimationUsingKeyFrames();
            translationAnimation.Duration = TimeSpan.FromSeconds(8.55);

            translationAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(pos1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));

            translationAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(pos2, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8))));

            translationAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(pos3, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8.5))));

            translationAnimation.KeyFrames.Add(new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8.55))));

            Storyboard.SetTargetProperty(translationAnimation, new PropertyPath(TranslateTransform.XProperty));

            Storyboard translationStoryboard = new Storyboard();
            translationStoryboard.Children.Add(translationAnimation);

            translationAnimation.Completed += delegate
            {
                if (loopCount > 0)
                {
                    bg.Source = new BitmapImage(new Uri(képekNeve[loopCount], UriKind.Relative));
                    animatedTranslateTransform.BeginAnimation(TranslateTransform.XProperty, translationAnimation);
                    loopCount--;
                }
                else
                {
                    router.ChangeCurrentPage(new MinigameSelectionPage());
                }
            };

            animatedTranslateTransform.BeginAnimation(TranslateTransform.XProperty, translationAnimation);

            loopCount--;

        }
    }
}
