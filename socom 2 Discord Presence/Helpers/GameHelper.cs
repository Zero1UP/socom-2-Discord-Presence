using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socom_2_Discord_Presence
{
    public static class GameHelper
    {
        public const string PLAYER_POINTER_ADDRESS = "20440C38";
        public const string GAME_ENDED_ADDRESS = "20694C44";  //May need to reset this to 0 after it ends, it seems to persist till the next game and doesn't reset when the player loads i
        public const string CURRENT_MAP_ADDRESS = "20436540"; // Text String of MapID, if not in a game then it is set to NONE
        public const string SEAL_WIN_COUNTER_ADDRESS = "20695388";
        public const string TERRORIST_WIN_COUNTER_ADDRESS = "2069539C";
        public const string ROOM_NAME_ADDRESS = "20E40DDE";
        public const string CUSTOM_MAP_ADDRESS = "200F71B0";


        public static List<MapDataModel> mapInfo = new List<MapDataModel>
        {
            { new MapDataModel("MP1","Blizzard","blizzard","MP26","Blizzard Day","blizzard_day")},
            { new MapDataModel("MP2","Frost Fire","frostfire","MP27","Frostfire Day","frostfire_day")},
            { new MapDataModel("MP5","Abandoned","abandoned","MP21","Abandoned Day","abandoned_day")},
            { new MapDataModel("MP73","Sand Storm","sandstorm","MP89","Sandstorm Day","sandstorm_day")},
            { new MapDataModel("MP7","Night Stalker","nightstalker","MP29","Nightstalker Day","nightstalker_day")},
            { new MapDataModel("MP6","Desert Glory","desertglory","MP28","Desert Glory Night","desertglory_night")},
            { new MapDataModel("M51","Seeding Chaos","seedingchaos","","","")},
            { new MapDataModel("M52","Terminal Transaction","terminaltransaction","","","")},
            { new MapDataModel("M53","Upland Assault","default","","","")},
            { new MapDataModel("M61","Urban Sweep","default","","","")},
            { new MapDataModel("M62","Strangle Hold","default","","","")},
            { new MapDataModel("M63","Hydro Electric","default","","","")},
            { new MapDataModel("M71","Guardian Angels","default","","","")},
            { new MapDataModel("M72","Protect and Serve","default","","","")},
            { new MapDataModel("M73","Against the Tide","default","","","")},
            { new MapDataModel("M81","Lockdown","default","","","")},
            { new MapDataModel("M82","Guided Tour","default","","","")},
            { new MapDataModel("M63","Doomsday Delivery","default","","","")},
            { new MapDataModel("NONE","","default","","","")},
            { new MapDataModel("MP10","Blood Lake","bloodlake","MP32","Blood Lake Night","bloodlake_night")},
            { new MapDataModel("MP11","Death Trap","deathtrap","MP33","Death Trap Night","deathtrap_night")},
            { new MapDataModel("MP12","The Ruins","theruins","MP34","The Ruins Night","theruins_night")},
            { new MapDataModel("MP62","Enowapi","enowapi","","","")},
            { new MapDataModel("MP8","Rat's Nest","ratsnest","MP30","Rat's Nest Day","ratsnest_day")},
            { new MapDataModel("MP53","Fox Hunt","foxhunt","","","")},
            { new MapDataModel("MP51","Vigilance","vigilance","","","")},
            { new MapDataModel("MP9","Bitter Jungle","bitterjungle","MP31","Bitter Jungle Night","bitterjungle_night")},
            { new MapDataModel("MP52","The Mixer","themixer","MP25","The Mixer Night","themixer_night")},
            { new MapDataModel("MP71","Fishhook","fishhook","MP23","Fish Hook Night","fishhook_night")},
            { new MapDataModel("MP72","Crossroads","crossroads","MP24","Crossroads Night","crossroads_night")},
            { new MapDataModel("MP64","Shadow Falls","shadowfalls","MP80","Shadowfalls Day","shadowfalls_day")},
            { new MapDataModel("MP61","Sujo","sujo","","","")},
            { new MapDataModel("MP81","Chain Reaction","chainreaction","","","")},
            { new MapDataModel("MP82","Guidanace","guidance","","","")},
            { new MapDataModel("MP83","Requim","requiem","","","")}
        };
    }
}
