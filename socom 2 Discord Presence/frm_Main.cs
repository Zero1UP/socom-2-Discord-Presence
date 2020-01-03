using DiscordRPC;
using Memory;
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

namespace socom_2_Discord_Presence
{
    public partial class frm_Main : Form
    {
        private const string PCSX2PROCESSNAME = "pcsx2";
        bool pcsx2Running;
        Mem m = new Mem();

        bool gameStarted = false;
        private static RichPresence presence = new RichPresence()
        {
            Details = "Not currently in a game.",
            State = "Room: Not in a room.",
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

        private void setPresence(string roomName, int sealWins, int terrorWins, string mapID, int customMap)
        {

            MapDataModel mapInfo = GameHelper.mapInfo.Find(x => x._mapID == mapID.ToUpper());

            if (sealWins == -1 && terrorWins == -1)
            {
                presence.Details = "Not currently in a game.";
            }
            else
            {
                presence.Details = "Seals: " + sealWins.ToString() + " || Terrorist: " + terrorWins.ToString();
            }

            presence.State = "Room: " + roomName;

            presence.Assets = new Assets();

            if (customMap == 1)
            {
                presence.Assets.LargeImageKey = mapInfo._altDiscordKey;
                presence.Assets.SmallImageKey = mapInfo._altDiscordKey;
                presence.Assets.LargeImageText = mapInfo._altMapName;
                presence.Assets.SmallImageText = mapInfo._altMapName;

            }
            else
            {
                presence.Assets.LargeImageKey = mapInfo._discordKey;
                presence.Assets.SmallImageKey = mapInfo._discordKey;
                presence.Assets.LargeImageText = mapInfo._mapName;
                presence.Assets.SmallImageText = mapInfo._mapName;
            }

            client.SetPresence(presence);
        }
        private void resetPresence()
        {
            presence.Timestamps = null;
            setPresence("Not in a room or in lobby", -1, -1, "NONE", 0);
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
            }
            else
            {
                lbl_PCSX2.Text = "Waiting for PCSX2...";
                lbl_PCSX2.ForeColor = Color.FromArgb(120, 120, 120);  
                pcsx2Running = false;
            }
        }

        private void tmr_GetPCSX2Data_Tick(object sender, EventArgs e)
        {
            if (pcsx2Running)
            {
                m.OpenProcess(PCSX2PROCESSNAME + ".exe");
                //Check to make sure that the user is even in a game to begin with
                if ((m.readBytes(GameHelper.PLAYER_POINTER_ADDRESS, 4) != null) && (!m.readBytes(GameHelper.PLAYER_POINTER_ADDRESS, 4).SequenceEqual(new byte[] { 0, 0, 0, 0 })))
                {
                    if (m.readByte(GameHelper.GAME_ENDED_ADDRESS) == 0)
                    {
                        string roomName = ByteConverstionHelper.convertBytesToString(m.readBytes(GameHelper.ROOM_NAME_ADDRESS, 22));
                        string mapID = ByteConverstionHelper.convertBytesToString(m.readBytes(GameHelper.CURRENT_MAP_ADDRESS, 4));
                        int customMap = m.readByte(GameHelper.CUSTOM_MAP_ADDRESS);
                        int sealsRoundsWon = m.readByte(GameHelper.SEAL_WIN_COUNTER_ADDRESS);
                        int terroristRoundsWon = m.readByte(GameHelper.TERRORIST_WIN_COUNTER_ADDRESS);
                        if (!gameStarted)
                        {
                            presence.Timestamps = new Timestamps()
                            {
                                Start = DateTime.UtcNow

                            };

                            gameStarted = true;
                        }
                        setPresence(roomName, sealsRoundsWon, terroristRoundsWon, mapID, customMap);
                    }
                    else
                    {
                        m.writeBytes(GameHelper.GAME_ENDED_ADDRESS, new byte[] { 0 });
                        resetPresence();
                    }
                }
                else
                {
                    m.writeBytes(GameHelper.GAME_ENDED_ADDRESS, new byte[] { 0 });
                    resetPresence();
                }
            }
        }
    }
}
