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
                 _________    ___    ____    ________    ___       __   _________    _________  
                |\    ____\  |\  \  |\   \  |\   __  \  |\  \     |  | |\   _____\  |\______  \
                \ \   \___|_ \ \  \  \ \  \ \ \  \|\  \ \ \  \    |  | \ \  \        \|_____\  \
                 \ \_____   \ \ \  \  \ \  \ \ \       \ \ \  \   |  |  \ \  \___            \  \
                  \|_____|\  \ \ \  \__\_\  \ \ \   _  _\ \ \  \  |  |   \ \   __\       __   \  \
                    _____|_\  \ \ \          \ \ \  \\  \  \ \  \ |  |    \ \  \______  |\ \___\  \
                   |\__________\ \_\__________\ \ \__\\ _\  \ \  \|  |     \ \________\ \ \________\
                   \|__________|  \|__________|  \|__|\|__|  \_\_____|      \|________|  \|________|";
            return survey;
        }
    }
}
