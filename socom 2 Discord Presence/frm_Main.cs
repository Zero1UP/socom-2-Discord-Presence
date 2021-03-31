using DiscordRPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Binarysharp.MemoryManagement;
namespace socom_2_Discord_Presence
{
    public partial class frm_Main : Form
    {
        private const string PCSX2PROCESSNAME = "pcsx2";
        bool pcsx2Running;
        MemorySharp m = null;

        bool gameStarted = false;
        private static RichPresence presence = new RichPresence()
        {
            Details = "Not currently in a game.",
            State = "",
            Assets = new Assets()
            {
                LargeImageKey = "default",
                LargeImageText = "Not in a room.",
                SmallImageKey = "default"
            }
        };
        public DiscordRpcClient client;

        public frm_Main()
        {
            InitializeComponent();

            client = new DiscordRpcClient("657060473849643018");

            client.Initialize();
            client.SetPresence(presence);
        }

        private void setPresence(int sealWins, int terrorWins, string mapID,short kills, short deaths)
        {

            MapDataModel mapInfo = GameHelper.mapInfo.Find(x => x._mapID == mapID.ToUpper());

            if (sealWins == -1 && terrorWins == -1)
            {
                presence.Details = "Not currently in a game.";
            }
            else
            {
                presence.Details = "Seals: " + sealWins + " || Terrorist: " + terrorWins;
                presence.State = "Kills: " + kills + " Deaths: " + deaths;
            }

            

            presence.Assets = new Assets();

            //if (customMap == 1)
            //{
            //    presence.Assets.LargeImageKey = mapInfo._altDiscordKey;
            //    presence.Assets.SmallImageKey = mapInfo._altDiscordKey;
            //    presence.Assets.LargeImageText = mapInfo._altMapName;
            //    presence.Assets.SmallImageText = mapInfo._altMapName;

            //}
            //else

            presence.Assets.LargeImageKey = mapInfo._discordKey;
            presence.Assets.SmallImageKey = mapInfo._discordKey;
            presence.Assets.LargeImageText = mapInfo._mapName;
            presence.Assets.SmallImageText = mapInfo._mapName;


            client.SetPresence(presence);
        }
        private void resetPresence()
        {
            presence.Timestamps = null;
            setPresence(-1, -1, "NONE",-1,-1);
            gameStarted = false;
        }
        private void tmr_CheckPCSX2_Tick(object sender, EventArgs e)
        {
            Process[] pcsx2 = Process.GetProcessesByName(PCSX2PROCESSNAME);


            if (pcsx2.Length > 0)
            {
                lbl_PCSX2.Text = "PCSX2 Detected";
                lbl_PCSX2.ForeColor = Color.FromArgb(20, 192, 90);
                pcsx2Running = true;
                return;
            }
            lbl_PCSX2.Text = "Waiting for PCSX2...";
            lbl_PCSX2.ForeColor = Color.FromArgb(120, 120, 120);
            pcsx2Running = false;
        }

        private void tmr_GetPCSX2Data_Tick(object sender, EventArgs e)
        {
            if (pcsx2Running)
            {
                m = new MemorySharp(Process.GetProcessesByName(PCSX2PROCESSNAME).First());

                
                try
                {
                    //Check to make sure that the user is even in a game to begin with
                    if ((m.Read<byte>(GameHelper.PLAYER_POINTER_ADDRESS, 4, false) != null) && (!m.Read<byte>(GameHelper.PLAYER_POINTER_ADDRESS, 4, false).SequenceEqual(new byte[] { 0, 0, 0, 0 })))
                    {
                        if (m.Read<byte>(GameHelper.GAME_ENDED_ADDRESS, false) == 0)
                        {
                            IntPtr playerObjectAddress = new IntPtr(m.Read<int>(GameHelper.PLAYER_POINTER_ADDRESS, false)) + 0x20000000;
                            short kills = m.Read<short>(playerObjectAddress + GameHelper.PLAYER_KILLS_OFFSET, false);
                            short deaths = m.Read<short>(playerObjectAddress + GameHelper.PLAYER_DEATHS_OFFSET, false);
                            string mapID = m.ReadString(GameHelper.CURRENT_MAP_ADDRESS, Encoding.Default, false, 4);
                            //int customMap = m.readByte(GameHelper.CUSTOM_MAP_ADDRESS);
                            int sealsRoundsWon = m.Read<byte>(GameHelper.SEAL_WIN_COUNTER_ADDRESS, false);
                            int terroristRoundsWon = m.Read<byte>(GameHelper.TERRORIST_WIN_COUNTER_ADDRESS, false);
                            if (!gameStarted)
                            {
                                presence.Timestamps = new Timestamps()
                                {
                                    Start = DateTime.UtcNow

                                };

                                gameStarted = true;
                            }
                            setPresence(sealsRoundsWon, terroristRoundsWon, mapID, kills, deaths);
                        }
                        else
                        {
                            m.Write<byte>(GameHelper.GAME_ENDED_ADDRESS, new byte[] { 0x00 }, false);
                            resetPresence();
                        }
                    }
                    else
                    {
                        m.Write<byte>(GameHelper.GAME_ENDED_ADDRESS, new byte[] { 0x00 }, false);
                        resetPresence();
                    }
                }
                catch (Exception)
                {
                    //This only happens if the game isn't actually running but pcsx2 is. It would result in a crash but there's no reason to inform the user
                    resetPresence();
                }

            }
        }
    }
}
