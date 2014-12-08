/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AnimationMaker"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using AnimationMaker.Model;
using AnimationMaker.Services;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace AnimationMaker.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.Register<IMessenger, Messenger>();
	        SimpleIoc.Default.Register<IAnimationSerializer, AnimationSerializer>();
	        SimpleIoc.Default.Register<IUserDialogService, UserDialogService>();
	        SimpleIoc.Default.Register<IFrameViewModelFactory, FrameViewModelFactory>();
	        SimpleIoc.Default.Register<IFigureViewModelFactory, FigureViewModelFactory>();
	        SimpleIoc.Default.Register<IAnimationViewModel, AnimationViewModel>();
        }

        public IAnimationViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IAnimationViewModel>();
            }
        }
        
        public static void Cleanup()
        {

        }
    }
}