/*
	Copyright 2011 Evolve Team
    
    This code is licensed under the binpress license; a copy
    of the license can be found below:
	
    http://www.binpress.com/license/view/l/6cfa4c36602b0f90ab898dc9fbd77b84
 
    The binpress license states that:
     - May only be used for personal use (cannot be resold or distributed)
     - Non-commerical use only
     - Cannot modify source code for any purpose (cannot create derivative works
    The implications of not following the license may lead to legal action, depending
    on the circumstances.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace MCLawl.Gui
{
    /// <summary>
    /// Static class permitting access to static FadeForm function
    /// </summary>
    public static class FadeEffect
    {
        /// <summary>
        /// Function used to fade out a form using a user defined number
        /// of steps
        /// </summary>
        /// <param name="f">The Windows form to fade out</param>
        /// <param name="NumberOfSteps">The number of steps used to fade the
        /// form</param>
        public static void FadeForm(System.Windows.Forms.Form f, byte NumberOfSteps)
        {
            float StepVal = (float)(100f / NumberOfSteps);
            float fOpacity = 100f;
            for (byte b = 0; b < NumberOfSteps; b++)
            {
                f.Opacity = fOpacity / 100;
                f.Refresh();
                fOpacity -= StepVal;
            }
        }
    }
}
