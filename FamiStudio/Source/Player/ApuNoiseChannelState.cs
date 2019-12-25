﻿namespace FamiStudio
{
    public class ApuNoiseChannelState : ChannelState
    {
        public ApuNoiseChannelState(int apuIdx, int channelIdx) : base(apuIdx, channelIdx)
        {
        }

        public override void UpdateAPU()
        {
            if (note.IsStop)
            {
                NesApu.NesApuWriteRegister(apuIdx, NesApu.APU_NOISE_VOL, 0xf0);
            }
            else if (note.IsValid)
            {
                var noteVal = (int)(((note.Value + envelopeValues[Envelope.Arpeggio]) & 0x0f) ^ 0x0f) | ((duty << 7) & 0x80);
                var volume  = MultiplyVolumes(note.Volume, envelopeValues[Envelope.Volume]);

                NesApu.NesApuWriteRegister(apuIdx, NesApu.APU_NOISE_LO, noteVal);
                NesApu.NesApuWriteRegister(apuIdx, NesApu.APU_NOISE_VOL, 0xf0 | volume);
            }
        }
    }
}
