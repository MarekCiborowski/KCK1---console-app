using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys
{
    public class ArtAscii
    {
        public static string GetMainTitleString()
        {
            string survey = @"
                 _________    ___    ____    ________    ___      __   _________    ___    __    
                |\    ____\  |\  \  |\   \  |\   __  \  |\  \    |  | |\   _____\  |\  \  |  | 
                \ \   \___|_ \ \  \  \ \  \ \ \  \|\  \ \ \  \   |  | \ \  \       \ \  \ |  |
                 \ \_____   \ \ \  \  \ \  \ \ \       \ \ \  \  |  |  \ \  \___    \ \  \|  |
                  \|_____|\  \ \ \  \__\_\  \ \ \   _  _\ \ \  \ |  |   \ \   __\    \ \_    |
                    _____|_\  \ \ \          \ \ \  \\  \  \ \  \|  |    \ \  \______ \_ |   |
                   |\__________\ \_\__________\ \ \__\\ _\  \_\_____|     \ \________\  ||___|
                   \|__________|  \|__________|  \|__|\|__|  \|_____|      \|________|  \|___|";
            return survey;
        }
    }
}
