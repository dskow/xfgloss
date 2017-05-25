/*
 * Copyright (C) 2017-2017 Dskow Publishing LLC & David Skowronski: 
 * https://dskow.github.io
 * http://twitter.com/dskowfl
 * http://dskow.com
 * 
 * The MIT License (MIT) see GitHub For more information
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ContentPage), typeof(XFGloss.UWP.Renderers.XFGlossContentPageRenderer))]

namespace XFGloss.UWP.Renderers
{
    /// <summary>
    /// The UWP platform-specific Xamarin.Forms renderer used for all <see cref="T:Xamarin.Forms.ContentPage"/>
    /// derived classes. Also implements the <see cref="XFGloss.IGradientRenderer" /> interface to support the added
    /// BackgroundGradient XFGloss property.
    /// </summary>
    public class XFGlossContentPageRenderer : PageRenderer, IGradientRenderer
    {
        #region IGradientRenderer implementation

        /// <summary>
        /// Implementation of method required by the <see cref="T:XFGloss.IXGlossRenderer"/> interface that the 
        /// <see cref="T:XFGloss.IgradientRenderer"/> interface extends. Indicates if there is an existing
        /// implementation of the property specified by the propertyName parameter.
        /// </summary>
        /// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
        /// <returns><c>true</c>, if an existing implementation is found, <c>false</c>, otherwise.</returns>
        public bool CanUpdate(string propertyName)
        {
            // No need to check property name yet, BackgroundGradient is the only one being handled here.
            // XFGlossGradientLayer.GetGradientLayer(NativeView) != null;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementation of method required by the <see cref="T:XFGloss.IXFGlossRenderer"/> interface that the
        /// <see cref="T:XFGloss.IGradientRenderer"/> interface extends. Applies the passed 
        /// <see cref="T:XFGloss.XFGlossElement"/> properties to the UWP UIView.
        /// </summary>
        /// <typeparam name="TElement">The type <see cref="T:XFGloss.XFGlossElement"/> that changed</typeparam>
        /// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
        /// <param name="element">The <see cref="T:XFGloss.XFGlossElement"/> instance that changed</param>
        public void CreateNativeElement<TElement>(string propertyName, TElement element) where TElement : XFGlossElement
        {
            if (element is Gradient)
            {
                // No need to check property name yet, BackgroundGradient is the only one being handled here.
                //XFGlossGradientLayer.CreateGradientLayer(NativeView, element as Gradient);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementation of method required by the <see cref="T:XFGloss.IXFGlossRenderer"/> interface that the
		/// <see cref="T:XFGloss.IGradientRenderer"/> interface extends. Removes any existing implementation of
		/// the property specified by the propertyName parameter.
        /// </summary>
        /// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
        public void RemoveNativeElement(string propertyName)
        {
            // No need to check property name yet, BackgroundGradient is the only one being handled here.
            //XFGlossGradientLayer.RemoveGradientLayer(NativeView);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementation of method required by the <see cref="T:XFGloss.IGradientRenderer"/> interface. Updates
		/// the rotation angle being used by any existing implementation of the property specified by the propertyName
		/// parameter.
        /// </summary>
        /// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
        /// <param name="rotation">The new rotation value, an integer number between 0 and 359</param>
        public void UpdateRotation(string propertyName, int rotation)
        {
            // No need to check property name yet, BackgroundGradient is the only one being handled here.
            //XFGlossGradientLayer.GetGradientLayer(NativeView)?.UpdateRotation(rotation);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementation of method required by the <see cref="T:XFGloss.IGradientRenderer"/> interface. Updates
		/// the gradient fill steps being used by any existing implementation of the property specified by the 
		/// propertyName parameter.
        /// </summary>
        /// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
        /// <param name="steps">The new collection of <see cref="T:XFGloss.GradientStep"/> instances that specify the
		/// colors and positions of each step of the gradient fill</param>
        public void UpdateSteps(string propertyName, GradientStepCollection steps)
        {
            // No need to check property name yet, BackgroundGradient is the only one being handled here.
            //XFGlossGradientLayer.GetGradientLayer(NativeView)?.UpdateSteps(steps);
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Dispose any created resources and prepare the instance for garbage collection
        /// </summary>
        /// <param name="disposing">If set to <c>true</c>, dispose any created resources</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
                if (bkgrndGradient != null)
                {
                    bkgrndGradient.DetachRenderer(this);
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// <see cref="T:Xamarin.Forms.Platform.UWP.PageRenderer"/> override that is called whenever the associated
        /// <see cref="T:Xamarin.Forms.ContentPage"/> instance changes
        /// </summary>
        /// <param name="e"><see cref="T:Xamarin.Forms.Platform.UWP.VisualElementChangedEventArgs"/> that specifies the
        /// previously and newly assigned <see cref="T:Xamarin.Forms.ContentPage"/> instances
        /// </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanging -= OnElementPropertyChanging;
                e.OldElement.PropertyChanged -= OnMyElementPropertyChanged;

                Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(e.OldElement);
                if (bkgrndGradient != null)
                {
                    bkgrndGradient.DetachRenderer(this);
                }
            }

            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanging += OnElementPropertyChanging;
                e.NewElement.PropertyChanged += OnMyElementPropertyChanged;

                Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
                if (bkgrndGradient != null)
                {
                    bkgrndGradient.AttachRenderer(ContentPageGloss.BackgroundGradientProperty.PropertyName,
                                                           this);
                }
            }
        }

        /// <summary>
        /// Private event handler that is called whenever a <see cref="T:Xamarin.Forms.BindableObject.PropertyChanging"/> 
        /// event is fired.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Property changing event arguments</param>
        void OnElementPropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs args)
        {
            if (args.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
            {
                Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
                if (bkgrndGradient != null)
                {
                    bkgrndGradient.DetachRenderer(this);
                }
            }
        }

        /// <summary>
        /// <see cref="T:Xamarin.Forms.Platform.iOS.PageRenderer"/> override that is called whenever the
        /// <see cref="T:Xamarin.Forms.ContentPage.PropertyChanged"/> event is fired
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Property changing event arguments</param>
        void OnMyElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == ContentPageGloss.BackgroundGradientProperty.PropertyName)
            {
                Gradient bkgrndGradient = ContentPageGloss.GetBackgroundGradient(Element);
                if (bkgrndGradient != null)
                {
                    bkgrndGradient.AttachRenderer(ContentPageGloss.BackgroundGradientProperty.PropertyName,
                                                           this);
                }
            }
        }
    }
}
