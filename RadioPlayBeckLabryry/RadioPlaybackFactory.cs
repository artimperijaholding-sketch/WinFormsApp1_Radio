using RadioPlayBeckLabrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadioPlayBeckLabryry;

public class RadioPlaybackFactory
{
    public IRadioPlayback GetInstance() => new RadioPlayback();
}
