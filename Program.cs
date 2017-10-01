using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using fNbt;
using Newtonsoft.Json;

namespace S2J
{
    class Program
    {
        static void Main(string[] args)
        {
            string file;
            //current version for 1.12.2
            double version = 1.122;
            #region GetFile
            if (args.Length > 0)
            {
                file = args[0];
            }
            else
            {
                Console.Write("请输入Schematic文件: ");
                file = Console.ReadLine();
                Console.WriteLine(file);
                Console.WriteLine();
            }
            if (file == null || !File.Exists(file))
            {
                Console.WriteLine("未找到文件");
                if (args.Contains("nopause")) return;
                Console.WriteLine("按任意键继续...");
                Console.ReadKey(true);
                Environment.Exit(1);
            }

            //get version
            foreach(string reg in args)
            {
                if(Regex.Match(reg, @"version=.*$").Success)
                {
                    if(Regex.Match(Regex.Matches(reg, @"version=.*$")[0].Value, @"\d*\.\d*").Success)
                    {
                        List<double> v1st = new List<double>();
                        foreach (Match res in Regex.Matches(Regex.Matches(reg, @"version=.*$")[0].Value, @"\d*\.\d*"))
                        {
                            v1st.Add(double.Parse(res.Value));
                        }
                        v1st.Sort(delegate (double x, double y)
                        {
                            return y.CompareTo(x);
                        });
                        version = v1st[0];
                    }
                }
            }
            #endregion
            try
            {
                #region Initialization
                var nbt = new NbtFile();
                nbt.LoadFromFile(file);
                var root = nbt.RootTag;

                var width = root.Get<NbtShort>("Width").ShortValue;
                var height = root.Get<NbtShort>("Height").ShortValue;
                var length = root.Get<NbtShort>("Length").ShortValue;
                var blocks = root.Get<NbtByteArray>("Blocks").ByteArrayValue;
                var blockdata = root.Get<NbtByteArray>("Data").ByteArrayValue;

                var model = new Model
                {
                    __comment =
                        "Create by S2J",
                    textures = new Dictionary<string, string>()
                };
                #endregion
                #region BlockLists
                    #region Layer
                        #region height=8
                        Dictionary<string, string> h8 = new Dictionary<string, string>();
                        h8.Add("1_0", "blocks/stone_andesite");
                        h8.Add("1_1", "blocks/stone_granite");
                        h8.Add("1_2", "blocks/stone_granite_smooth");
                        h8.Add("1_3", "blocks/stone_diorite");
                        h8.Add("1_4", "blocks/stone_diorite_smooth");
                        h8.Add("1_5", "blocks/stone_andesite");
                        h8.Add("1_6", "blocks/stone_andesite_smooth");
                        h8.Add("2_0", "blocks/grass_path_top");
                        h8.Add("3_0", "blocks/dirt");
                        h8.Add("3_1", "blocks/coarse_dirt");
                        h8.Add("3_2", "blocks/dirt_podzol_top");
                        h8.Add("4_0", "blocks/cobblestone");
                        h8.Add("5_0", "blocks/planks_oak");
                        h8.Add("5_1", "blocks/planks_spruce");
                        h8.Add("5_2", "blocks/planks_birch");
                        h8.Add("5_3", "blocks/planks_jungle");
                        h8.Add("5_4", "blocks/planks_acacia");
                        h8.Add("5_5", "blocks/planks_big_oak");
                        h8.Add("7_0", "blocks/bedrock");
                        h8.Add("12_0", "blocks/sand");
                        h8.Add("12_1", "blocks/red_sand");
                        h8.Add("13_0", "blocks/gravel");
                        h8.Add("14_0", "blocks/gold_ore");
                        h8.Add("15_0", "blocks/iron_ore");
                        h8.Add("16_0", "blocks/coal_ore");
                        h8.Add("17_0", "blocks/log_oak");
                        h8.Add("17_1", "blocks/log_spruce");
                        h8.Add("17_2", "blocks/log_birch");
                        h8.Add("17_3", "blocks/log_jungle");
                        h8.Add("18_0", "colormap/foliage");
                        h8.Add("18_1", "colormap/foliage");
                        h8.Add("18_2", "colormap/foliage");
                        h8.Add("18_3", "colormap/foliage");
                        h8.Add("18_4", "colormap/foliage");
                        h8.Add("18_5", "colormap/foliage");
                        h8.Add("18_6", "colormap/foliage");
                        h8.Add("18_7", "colormap/foliage");
                        h8.Add("18_8", "colormap/foliage");
                        h8.Add("18_9", "colormap/foliage");
                        h8.Add("18_10", "colormap/foliage");
                        h8.Add("18_11", "colormap/foliage");
                        h8.Add("18_12", "colormap/foliage");
                        h8.Add("18_13", "colormap/foliage");
                        h8.Add("18_14", "colormap/foliage");
                        h8.Add("18_15", "colormap/foliage");
                        h8.Add("19_0", "blocks/sponge");
                        h8.Add("19_1", "blocks/sponge_wet");
                        h8.Add("20_0", "blocks/glass");
                        h8.Add("21_0", "blocks/lapis_ore");
                        h8.Add("22_0", "blocks/lapis_block");
                        h8.Add("23_0", "blocks/furnace_top");
                        h8.Add("24_0", "blocks/sandstone_normal");
                        h8.Add("24_1", "blocks/sandstone_carved");
                        h8.Add("24_2", "blocks/sandstone_smooth");
                        h8.Add("25_0", "blocks/noteblock");
                        h8.Add("29_0", "blocks/piston_top_sticky");
                        h8.Add("30_0", "blocks/web");
                        h8.Add("33_0", "blocks/piston_top_normal");
                        h8.Add("35_0", "blocks/wool_colored_white");
                        h8.Add("35_1", "blocks/wool_colored_orange");
                        h8.Add("35_2", "blocks/wool_colored_magenta");
                        h8.Add("35_3", "blocks/wool_colored_light_blue");
                        h8.Add("35_4", "blocks/wool_colored_yellow");
                        h8.Add("35_5", "blocks/wool_colored_lime");
                        h8.Add("35_6", "blocks/wool_colored_pink");
                        h8.Add("35_7", "blocks/wool_colored_gray");
                        h8.Add("35_8", "blocks/wool_colored_silver");
                        h8.Add("35_9", "blocks/wool_colored_cyan");
                        h8.Add("35_10", "blocks/wool_colored_purple");
                        h8.Add("35_11", "blocks/wool_colored_blue");
                        h8.Add("35_12", "blocks/wool_colored_brown");
                        h8.Add("35_13", "blocks/wool_colored_green");
                        h8.Add("35_14", "blocks/wool_colored_red");
                        h8.Add("35_15", "blocks/wool_colored_black");
                        h8.Add("41_0", "blocks/gold_block");
                        h8.Add("42_0", "blocks/iron_block");
                        h8.Add("43_0", "blocks/stone_slab_top");
                        h8.Add("43_1", "blocks/sandstone_top");
                        h8.Add("43_3", "blocks/cobblestone");
                        h8.Add("43_4", "blocks/brick");
                        h8.Add("43_5", "blocks/stonebrick");
                        h8.Add("43_6", "blocks/nether_brick");
                        h8.Add("43_7", "blocks/quartz_block_top");
                        h8.Add("125_0", "blocks/planks_oak");
                        h8.Add("125_1", "blocks/planks_spruce");
                        h8.Add("125_2", "blocks/planks_birch");
                        h8.Add("125_3", "blocks/planks_jungle");
                        h8.Add("125_4", "blocks/planks_acacia");
                        h8.Add("125_5", "blocks/planks_big_oak");
                        h8.Add("181_0", "blocks/red_sandstone_top");
                        h8.Add("204_0", "blocks/purpur_block");
                        h8.Add("45_0", "blocks/brick");
                        h8.Add("46_0", "blocks/tnt_side");
                        h8.Add("47_0", "blocks/bookshelf");
                        h8.Add("48_0", "blocks/cobblestone_mossy");
                        h8.Add("49_0", "blocks/obsidian");
                        h8.Add("53_x", "blocks/planks_oak");
                        h8.Add("56_0", "blocks/diamond_ore");
                        h8.Add("57_0", "blocks/diamond_block");
                        h8.Add("58_0", "blocks/crafting_table_top");
                        h8.Add("60_x", "blocks/farmland_dry");
                        h8.Add("61_0", "blocks/furnace_top");
                        h8.Add("67_x", "blocks/cobblestone");
                        h8.Add("73_0", "blocks/redstone_ore");
                        h8.Add("78_7", "blocks/snow");
                        h8.Add("79_0", "blocks/ice");
                        h8.Add("80_0", "blocks/snow");
                        h8.Add("81_0", "blocks/cactus_side");
                        h8.Add("82_0", "blocks/clay");
                        h8.Add("84_0", "blocks/jukebox_side");
                        h8.Add("86_0", "blocks/pumpkin_side");
                        h8.Add("87_0", "blocks/netherrack");
                        h8.Add("88_0", "blocks/soul_sand");
                        h8.Add("89_0", "blocks/glowstone");
                        h8.Add("91_0", "blocks/pumpkin_side");
                        h8.Add("95_0", "blocks/glass_white");
                        h8.Add("95_1", "blocks/glass_orange");
                        h8.Add("95_2", "blocks/glass_magenta");
                        h8.Add("95_3", "blocks/glass_light_blue");
                        h8.Add("95_4", "blocks/glass_yellow");
                        h8.Add("95_5", "blocks/glass_lime");
                        h8.Add("95_6", "blocks/glass_pink");
                        h8.Add("95_7", "blocks/glass_gray");
                        h8.Add("95_8", "blocks/glass_light");
                        h8.Add("95_9", "blocks/glass_cyan");
                        h8.Add("95_10", "blocks/glass_purple");
                        h8.Add("95_11", "blocks/glass_blue");
                        h8.Add("95_12", "blocks/glass_brown");
                        h8.Add("95_13", "blocks/glass_green");
                        h8.Add("95_14", "blocks/glass_red");
                        h8.Add("95_15", "blocks/glass_black");
                        h8.Add("97_0", "blocks/stone");
                        h8.Add("97_1", "blocks/cobblestone");
                        h8.Add("97_2", "blocks/stonebrick");
                        h8.Add("97_3", "blocks/stonebrick_mossy");
                        h8.Add("97_4", "blocks/stonebrick_cracked");
                        h8.Add("97_5", "blocks/stonebrick_carved");
                        h8.Add("98_0", "blocks/stonebrick");
                        h8.Add("98_1", "blocks/stonebrick_mossy");
                        h8.Add("98_2", "blocks/stonebrick_cracked");
                        h8.Add("98_3", "blocks/stonebrick_carved");
                        h8.Add("99_0", "blocks/mushroom_block_skin_brown");
                        h8.Add("100_0", "blocks/mushroom_block_skin_red");
                        h8.Add("103_0", "blocks/melon_side");
                        h8.Add("108_x", "blocks/brick");
                        h8.Add("109_x", "blocks/stonebrick");
                        h8.Add("110_0", "blocks/mycelium_top");
                        h8.Add("112_0", "blocks/nether_brick");
                        h8.Add("114_x", "blocks/nether_brick");
                        h8.Add("120_0", "blocks/endframe_top");
                        h8.Add("121_0", "blocks/end_stone");
                        h8.Add("122_0", "blocks/dragon_egg");
                        h8.Add("123_0", "blocks/redstone_lamp_on");
                        h8.Add("124_0", "blocks/redstone_lamp_off");
                        h8.Add("128_x", "blocks/sandstone_normal");
                        h8.Add("129_0", "blocks/emerald_ore");
                        h8.Add("133_0", "blocks/emerald_block");
                        h8.Add("134_x", "blocks/planks_spruce");
                        h8.Add("135_x", "blocks/planks_birch");
                        h8.Add("136_x", "blocks/planks_jungle");
                        h8.Add("137_0", "blocks/command_block_front");
                        h8.Add("138_0", "blocks/beacon");
                        h8.Add("139_0", "blocks/cobblestone");
                        h8.Add("139_1", "blocks/cobblestone_mossy");
                        h8.Add("152_0", "blocks/redstone_block");
                        h8.Add("153_0", "blocks/quartz_ore");
                        h8.Add("155_0", "blocks/quartz_block_side");
                        h8.Add("155_1", "blocks/quartz_block_chiseled");
                        h8.Add("155_2", "blocks/quartz_block_lines");
                        h8.Add("156_x", "blocks/quartz_block");
                        h8.Add("158_0", "blocks/furnace_side");
                        h8.Add("159_0", "blocks/hardened_clay_stained_white");
                        h8.Add("159_1", "blocks/hardened_clay_stained_orange");
                        h8.Add("159_2", "blocks/hardened_clay_stained_magenta");
                        h8.Add("159_3", "blocks/hardened_clay_stained_light_blue");
                        h8.Add("159_4", "blocks/hardened_clay_stained_yellow");
                        h8.Add("159_5", "blocks/hardened_clay_stained_lime");
                        h8.Add("159_6", "blocks/hardened_clay_stained_pink");
                        h8.Add("159_7", "blocks/hardened_clay_stained_gray");
                        h8.Add("159_8", "blocks/hardened_clay_stained_silver");
                        h8.Add("159_9", "blocks/hardened_clay_stained_cyan");
                        h8.Add("159_10", "blocks/hardened_clay_stained_purple");
                        h8.Add("159_11", "blocks/hardened_clay_stained_blue");
                        h8.Add("159_12", "blocks/hardened_clay_stained_brown");
                        h8.Add("159_13", "blocks/hardened_clay_stained_green");
                        h8.Add("159_14", "blocks/hardened_clay_stained_red");
                        h8.Add("159_15", "blocks/hardened_clay_stained_black");
                        h8.Add("161_0", "colormap/foliage");
                        h8.Add("161_1", "colormap/foliage");
                        h8.Add("161_4", "colormap/foliage");
                        h8.Add("161_5", "colormap/foliage");
                        h8.Add("161_8", "colormap/foliage");
                        h8.Add("161_9", "colormap/foliage");
                        h8.Add("161_12", "colormap/foliage");
                        h8.Add("161_13", "colormap/foliage");
                        h8.Add("162_0", "blocks/log_acacia");
                        h8.Add("162_1", "blocks/log_big_oak");
                        h8.Add("163_x", "blocks/planks_acacia");
                        h8.Add("164_x", "blocks/planks_big_oak");
                        h8.Add("165_0", "blocks/slime");
                        h8.Add("168_0", "blocks/prismarine_rough");
                        h8.Add("168_1", "blocks/prismarine_bricks");
                        h8.Add("168_2", "blocks/prismarine_dark");
                        h8.Add("169_0", "blocks/sea_lantern");
                        h8.Add("170_0", "blocks/hay_block_side");
                        h8.Add("172_0", "blocks/hardened_clay");
                        h8.Add("173_0", "blocks/coal_block");
                        h8.Add("174_0", "blocks/ice_packed");
                        h8.Add("179_0", "blocks/red_sandstone_normal");
                        h8.Add("179_1", "blocks/red_sandstone_carved");
                        h8.Add("179_2", "blocks/red_sandstone_smooth");
                        h8.Add("180_x", "blocks/red_sandstone_normal");
                        h8.Add("199_0", "blocks/chorus_plant");
                        h8.Add("200_0", "blocks/chorus_flower");
                        h8.Add("201_0", "blocks/purpur_block");
                        h8.Add("202_0", "blocks/purpur_pillar");
                        h8.Add("203_x", "blocks/purpur_block");
                        h8.Add("206_0", "blocks/end_bricks");
                        h8.Add("213_0", "blocks/magma");
                        h8.Add("214_0", "blocks/nether_wart_block");
                        h8.Add("215_0", "blocks/red_nether_brick");
                        h8.Add("216_0", "blocks/bone_block_side");
                        h8.Add("218_0", "blocks/furnace_top");
                        h8.Add("219_0", "blocks/shulker_top_white");
                        h8.Add("220_0", "blocks/shulker_top_orange");
                        h8.Add("221_0", "blocks/shulker_top_magenta");
                        h8.Add("222_0", "blocks/shulker_top_light_blue");
                        h8.Add("223_0", "blocks/shulker_top_yellow");
                        h8.Add("224_0", "blocks/shulker_top_lime");
                        h8.Add("225_0", "blocks/shulker_top_pink");
                        h8.Add("226_0", "blocks/shulker_top_gray");
                        h8.Add("227_0", "blocks/shulker_top_silver");
                        h8.Add("228_0", "blocks/shulker_top_cyan");
                        h8.Add("229_0", "blocks/shulker_top_purple");
                        h8.Add("230_0", "blocks/shulker_top_blue");
                        h8.Add("231_0", "blocks/shulker_top_brown");
                        h8.Add("232_0", "blocks/shulker_top_green");
                        h8.Add("233_0", "blocks/shulker_top_red");
                        h8.Add("234_0", "blocks/shulker_top_black");
                        h8.Add("235_0", "blocks/glazed_terracotta_white");
                        h8.Add("236_0", "blocks/glazed_terracotta_orange");
                        h8.Add("237_0", "blocks/glazed_terracotta_magenta");
                        h8.Add("238_0", "blocks/glazed_terracotta_light_blue");
                        h8.Add("239_0", "blocks/glazed_terracotta_yellow");
                        h8.Add("240_0", "blocks/glazed_terracotta_lime");
                        h8.Add("241_0", "blocks/glazed_terracotta_pink");
                        h8.Add("242_0", "blocks/glazed_terracotta_gray");
                        h8.Add("243_0", "blocks/glazed_terracotta_silver");
                        h8.Add("244_0", "blocks/glazed_terracotta_cyan");
                        h8.Add("245_0", "blocks/glazed_terracotta_purple");
                        h8.Add("246_0", "blocks/glazed_terracotta_blue");
                        h8.Add("247_0", "blocks/glazed_terracotta_brown");
                        h8.Add("248_0", "blocks/glazed_terracotta_green");
                        h8.Add("249_0", "blocks/glazed_terracotta_red");
                        h8.Add("250_0", "blocks/glazed_terracotta_black");
                        h8.Add("251_0", "blocks/concrete_white");
                        h8.Add("251_1", "blocks/concrete_orange");
                        h8.Add("251_2", "blocks/concrete_magenta");
                        h8.Add("251_3", "blocks/concrete_light_blue");
                        h8.Add("251_4", "blocks/concrete_yellow");
                        h8.Add("251_5", "blocks/concrete_lime");
                        h8.Add("251_6", "blocks/concrete_pink");
                        h8.Add("251_7", "blocks/concrete_gray");
                        h8.Add("251_8", "blocks/concrete_silver");
                        h8.Add("251_9", "blocks/concrete_cyan");
                        h8.Add("251_10", "blocks/concrete_purple");
                        h8.Add("251_11", "blocks/concrete_blue");
                        h8.Add("251_12", "blocks/concrete_brown");
                        h8.Add("251_13", "blocks/concrete_green");
                        h8.Add("251_14", "blocks/concrete_red");
                        h8.Add("251_15", "blocks/concrete_black");
                        h8.Add("252_0", "blocks/concrete_powder_white");
                        h8.Add("252_1", "blocks/concrete_powder_orange");
                        h8.Add("252_2", "blocks/concrete_powder_magenta");
                        h8.Add("252_3", "blocks/concrete_powder_light_blue");
                        h8.Add("252_4", "blocks/concrete_powder_yellow");
                        h8.Add("252_5", "blocks/concrete_powder_lime");
                        h8.Add("252_6", "blocks/concrete_powder_pink");
                        h8.Add("252_7", "blocks/concrete_powder_gray");
                        h8.Add("252_8", "blocks/concrete_powder_silver");
                        h8.Add("252_9", "blocks/concrete_powder_cyan");
                        h8.Add("252_10", "blocks/concrete_powder_purple");
                        h8.Add("252_11", "blocks/concrete_powder_blue");
                        h8.Add("252_12", "blocks/concrete_powder_brown");
                        h8.Add("252_13", "blocks/concrete_powder_green");
                        h8.Add("252_14", "blocks/concrete_powder_red");
                        h8.Add("252_15", "blocks/concrete_powder_black");
                        h8.Add("255_0", "blocks/structure_block");
                        //water
                        h8.Add("8_0", "blocks/water_flow");
                        h8.Add("9_0", "blocks/water_still");
                        //lava
                        h8.Add("10_0", "blocks/lava_flow");
                        h8.Add("11_0", "blocks/lava_still");
                        #endregion
                        #region height=7
                        Dictionary<string, string> h7 = new Dictionary<string, string>();
                        //water
                        h7.Add("8_7", "blocks/water_flow");
                        h7.Add("9_7", "blocks/water_still");
                        //lava
                        h7.Add("10_7", "blocks/lava_flow");
                        h7.Add("11_7", "blocks/lava_still");
                        //snow
                        h7.Add("78_6", "blocks/snow");
                        //piston_head
                        h7.Add("34_x", "blocks/piston_top_normal");
                        //frie
                        h7.Add("51_x", "blocks/fire_layer_0");
                        //cauldron
                        h7.Add("118_x", "blocks/cauldron_inner");
                        //hopper
                        h7.Add("154_0", "blocks/hopper_top");
                        #endregion
                        #region height=6
                        Dictionary<string, string> h6 = new Dictionary<string, string>();
                        //water
                        h6.Add("8_6", "blocks/water_flow");
                        h6.Add("9_6", "blocks/water_still");
                        //lava
                        h6.Add("10_6", "blocks/lava_flow");
                        h6.Add("11_6", "blocks/lava_still");
                        //snow
                        h6.Add("78_5", "blocks/snow");
                        //anvil
                        h6.Add("145_0", "blocks/anvil_top_damaged_0");
                        h6.Add("145_1", "blocks/anvil_top_damaged_1");
                        h6.Add("145_2", "blocks/anvil_top_damaged_2");
                        #endregion
                        #region height=5
                        Dictionary<string, string> h5 = new Dictionary<string, string>();
                        //water
                        h5.Add("8_5", "blocks/water_flow");
                        h5.Add("9_5", "blocks/water_still");
                        //lava
                        h5.Add("10_5", "blocks/lava_flow");
                        h5.Add("11_5", "blocks/lava_still");
                        //snow
                        h5.Add("78_4", "blocks/snow");
                        #endregion
                        #region height=4,bottom
                        Dictionary<string, string> h4_bottom = new Dictionary<string, string>();
                        h4_bottom.Add("44_0", "blocks/stone_slab_top");
                        h4_bottom.Add("44_1", "blocks/sandstone_top");
                        h4_bottom.Add("44_3", "blocks/cobblestone");
                        h4_bottom.Add("44_4", "blocks/brick");
                        h4_bottom.Add("44_5", "blocks/stonebrick");
                        h4_bottom.Add("44_6", "blocks/nether_brick");
                        h4_bottom.Add("44_7", "blocks/quartz_block_top");
                        h4_bottom.Add("126_0", "blocks/planks_oak");
                        h4_bottom.Add("126_1", "blocks/planks_spruce");
                        h4_bottom.Add("126_2", "blocks/planks_birch");
                        h4_bottom.Add("126_3", "blocks/planks_jungle");
                        h4_bottom.Add("126_4", "blocks/planks_acacia");
                        h4_bottom.Add("126_5", "blocks/planks_big_oak");
                        h4_bottom.Add("182_0", "blocks/red_sandstone_top");
                        h4_bottom.Add("205_0", "blocks/purpur_block");
                        //water
                        h4_bottom.Add("8_4", "blocks/water_flow");
                        h4_bottom.Add("9_4", "blocks/water_still");
                        //lava
                        h4_bottom.Add("10_4", "blocks/lava_flow");
                        h4_bottom.Add("11_4", "blocks/lava_still");
                        //snow
                        h4_bottom.Add("78_3", "blocks/snow");
                        //enchanting_table
                        h4_bottom.Add("116_x", "blocks/enchanting_table_bottom");
                        #endregion
                        #region height=4,top
                        Dictionary<string, string> h4_top = new Dictionary<string, string>();
                        h4_top.Add("44_8", "blocks/stone_slab_top");
                        h4_top.Add("44_9", "blocks/sandstone_top");
                        h4_top.Add("44_10", "blocks/planks_oak");
                        h4_top.Add("44_11", "blocks/cobblestone");
                        h4_top.Add("44_12", "blocks/brick");
                        h4_top.Add("44_13", "blocks/stonebrick");
                        h4_top.Add("44_14", "blocks/nether_brick");
                        h4_top.Add("44_15", "blocks/quartz_block_top");
                        h4_top.Add("126_8", "blocks/planks_oak");
                        h4_top.Add("126_9", "blocks/planks_spruce");
                        h4_top.Add("126_10", "blocks/planks_birch");
                        h4_top.Add("126_11", "blocks/planks_jungle");
                        h4_top.Add("126_12", "blocks/planks_acacia");
                        h4_top.Add("126_13", "blocks/planks_big_oak");
                        h4_top.Add("182_8", "blocks/red_sandstone_top");
                        h4_top.Add("205_8", "blocks/purpur_block");
                        #endregion
                        #region height=3
                        Dictionary<string, string> h3 = new Dictionary<string, string>();
                        //water
                        h3.Add("8_3", "blocks/water_flow");
                        h3.Add("9_3", "blocks/water_still");
                        //lava
                        h3.Add("10_3", "blocks/lava_flow");
                        h3.Add("11_3", "blocks/lava_still");
                        //snow
                        h3.Add("78_2", "blocks/snow");
                        //daylight_detector
                        h3.Add("151_x", "blocks/daylight_detector_top");
                        #endregion
                        #region height=2
                        Dictionary<string, string> h2 = new Dictionary<string, string>();
                        //water
                        h2.Add("8_2", "blocks/water_flow");
                        h2.Add("9_2", "blocks/water_still");
                        //lava
                        h2.Add("10_2", "blocks/lava_flow");
                        h2.Add("11_2", "blocks/lava_still");
                        //snow
                        h2.Add("78_1", "blocks/snow");
                        //bed
                        if (version >= 1.12)
                        {
                            h2.Add("26_0", "entity/bed/red");
                            h2.Add("26_1", "entity/bed/red");
                            h2.Add("26_2", "entity/bed/red");
                            h2.Add("26_3", "entity/bed/red");
                            h2.Add("26_4", "entity/bed/red");
                            h2.Add("26_5", "entity/bed/red");
                            h2.Add("26_6", "entity/bed/red");
                            h2.Add("26_7", "entity/bed/red");
                            h2.Add("26_8", "entity/bed/red");
                            h2.Add("26_9", "entity/bed/red");
                            h2.Add("26_10", "entity/bed/red");
                            h2.Add("26_11", "entity/bed/red");
                            h2.Add("26_12", "entity/bed/red");
                            h2.Add("26_13", "entity/bed/red");
                            h2.Add("26_14", "entity/bed/red");
                            h2.Add("26_15", "entity/bed/red");
                        }
                        else
                        {
                            h2.Add("26_0", "blocks/bed_feet_top");
                            h2.Add("26_1", "blocks/bed_feet_top");
                            h2.Add("26_2", "blocks/bed_feet_top");
                            h2.Add("26_3", "blocks/bed_feet_top");
                            h2.Add("26_4", "blocks/bed_feet_top");
                            h2.Add("26_5", "blocks/bed_feet_top");
                            h2.Add("26_6", "blocks/bed_feet_top");
                            h2.Add("26_7", "blocks/bed_feet_top");
                            h2.Add("26_8", "blocks/bed_head_top");
                            h2.Add("26_9", "blocks/bed_head_top");
                            h2.Add("26_10", "blocks/bed_head_top");
                            h2.Add("26_11", "blocks/bed_head_top");
                            h2.Add("26_12", "blocks/bed_head_top");
                            h2.Add("26_13", "blocks/bed_head_top");
                            h2.Add("26_14", "blocks/bed_head_top");
                            h2.Add("26_15", "blocks/bed_head_top");
                        }
                        #endregion
                        #region height=1
                        Dictionary<string, string> h1 = new Dictionary<string, string>();
                        //water
                        h1.Add("8_1", "blocks/water_flow");
                        h1.Add("9_1", "blocks/water_still");
                        //lava
                        h1.Add("10_1", "blocks/lava_flow");
                        h1.Add("11_1", "blocks/lava_still");
                        //carpet
                        h1.Add("171_0", "blocks/wool_colored_white");
                        h1.Add("171_1", "blocks/wool_colored_orange");
                        h1.Add("171_2", "blocks/wool_colored_magenta");
                        h1.Add("171_3", "blocks/wool_colored_light_blue");
                        h1.Add("171_4", "blocks/wool_colored_yellow");
                        h1.Add("171_5", "blocks/wool_colored_lime");
                        h1.Add("171_6", "blocks/wool_colored_pink");
                        h1.Add("171_7", "blocks/wool_colored_gray");
                        h1.Add("171_8", "blocks/wool_colored_silver");
                        h1.Add("171_9", "blocks/wool_colored_cyan");
                        h1.Add("171_10", "blocks/wool_colored_purple");
                        h1.Add("171_11", "blocks/wool_colored_blue");
                        h1.Add("171_12", "blocks/wool_colored_brown");
                        h1.Add("171_13", "blocks/wool_colored_green");
                        h1.Add("171_14", "blocks/wool_colored_red");
                        h1.Add("171_15", "blocks/wool_colored_black");
                        //snow
                        h1.Add("78_0", "blocks/snow");
                        #endregion
                        #region height=1,gap
                        Dictionary<string, string> h1_gap = new Dictionary<string, string>();
                        //pressure_plate
                        h1_gap.Add("70_x", "blocks/stone_andesite");
                        h1_gap.Add("72_x", "blocks/planks_oak");
                        h1_gap.Add("147_x", "blocks/gold_block");
                        h1_gap.Add("148_x", "blocks/iron_block");
                        //repeater
                        h1_gap.Add("93_x", "blocks/stone_slab_top");
                        h1_gap.Add("94_x", "blocks/stone_slab_top");
                        //comparator
                        h1_gap.Add("149_x", "blocks/stone_slab_top");
                        h1_gap.Add("150_x", "blocks/stone_slab_top");
                        //trapdoor
                        h1_gap.Add("96_x", "blocks/planks_oak");
                        h1_gap.Add("167_x", "blocks/iron_block");
                        //waterlily
                        h1_gap.Add("111_x", "colormap/foliage");
                        #endregion
                    #endregion
                    #region Connector
                    Dictionary<string, string> banConnect = new Dictionary<string, string>();
                        banConnect.Add("0_0", "air");
                        #region glass_pane & iron_bar
                        Dictionary<string, string> c1 = new Dictionary<string, string>();
						c1.Add("102_0", "blocks/glass");
                        c1.Add("160_0", "blocks/glass_white");
                        c1.Add("160_1", "blocks/glass_orange");
                        c1.Add("160_2", "blocks/glass_magenta");
                        c1.Add("160_3", "blocks/glass_light_blue");
                        c1.Add("160_4", "blocks/glass_yellow");
                        c1.Add("160_5", "blocks/glass_lime");
                        c1.Add("160_6", "blocks/glass_pink");
                        c1.Add("160_7", "blocks/glass_gray");
                        c1.Add("160_8", "blocks/glass_silver");
                        c1.Add("160_9", "blocks/glass_cyan");
                        c1.Add("160_10", "blocks/glass_purple");
                        c1.Add("160_11", "blocks/glass_blue");
                        c1.Add("160_12", "blocks/glass_brown");
                        c1.Add("160_13", "blocks/glass_green");
                        c1.Add("160_14", "blocks/glass_red");
                        c1.Add("160_15", "blocks/glass_black");
                        #endregion
                        #region fence
                        Dictionary<string, string> c2 = new Dictionary<string, string>();
                        //fence
                        c2.Add("85_0", "blocks/planks_oak");
                        c2.Add("113_0", "blocks/nether_brick");
                        c2.Add("188_0", "blocks/planks_spruce");
                        c2.Add("189_0", "blocks/planks_birch");
                        c2.Add("190_0", "blocks/planks_jungle");
                        c2.Add("191_0", "blocks/planks_big_oak");
                        c2.Add("192_0", "blocks/planks_acacia");
                        //fence_gate
                        c2.Add("107_x", "blocks/planks_oak");
                        c2.Add("183_x", "blocks/planks_spruce");
                        c2.Add("184_x", "blocks/planks_birch");
                        c2.Add("185_x", "blocks/planks_jungle");
                        c2.Add("186_x", "blocks/planks_big_oak");
                        c2.Add("187_x", "blocks/planks_acacia");
                        #endregion
                    #endregion
                #endregion
                #region attach blocks to textures
                foreach (var item in h1) { model.textures.Add(item.Key, item.Value); banConnect.Add(item.Key, item.Value); }
                foreach (var item in h1_gap) { model.textures.Add(item.Key, item.Value); banConnect.Add(item.Key, item.Value); }
                foreach (var item in h2) { model.textures.Add(item.Key, item.Value); banConnect.Add(item.Key, item.Value); }
                foreach (var item in h3) { model.textures.Add(item.Key, item.Value); banConnect.Add(item.Key, item.Value); }
                foreach (var item in h4_bottom) { model.textures.Add(item.Key, item.Value); }
                foreach (var item in h4_top) { model.textures.Add(item.Key, item.Value); }
                foreach (var item in h5) { model.textures.Add(item.Key, item.Value); banConnect.Add(item.Key, item.Value); }
                foreach (var item in h6) { model.textures.Add(item.Key, item.Value); banConnect.Add(item.Key, item.Value); }
                foreach (var item in h7) { model.textures.Add(item.Key, item.Value); banConnect.Add(item.Key, item.Value); }
                foreach (var item in h8) { model.textures.Add(item.Key, item.Value); }
                foreach (var item in c1) { model.textures.Add(item.Key, item.Value); }
                foreach (var item in c2) { model.textures.Add(item.Key, item.Value); }
                #endregion
                var elements = new List<Model.Element>();
                var random = new Random();
                var usedTextures = new List<string>();
                #region InputInfo
                Console.WriteLine(Environment.NewLine + "Info:");
                //mode
                bool[] modeInfo = { false, false };
                if (args.Contains("unlimit")) modeInfo[0] = true;
                if (args.Contains("smooth")) modeInfo[1] = true;
                //output size
                Console.WriteLine("width = " + width + " ,height = " + height + " ,length = " + length);
                //output mode
                Console.WriteLine("unlimit = " + modeInfo[0].ToString() + " ,smooth = " + modeInfo[1].ToString());
                Console.WriteLine(Environment.NewLine + "转换中...");
                #endregion
                for (var x = 0; x < width; ++x)
                {
                    for (var y = 0; y < height; ++y)
                    {
                        for (var z = 0; z < length; ++z)
                        {
                            //*define
                            var index = y * width * length + z * width + x;
                            var block = blocks[index];
                            var data = blockdata[index];
                            //*base
                            string texture = "", bmp = "";
                            var modelType = 0;//0-2:Layer, 3-4:Connector
                            var h = 0;

                            //match block for its texture
                            var exaDV = block.ToString() + "_" + data.ToString();
                            var dimDV = block.ToString() + "_x";

                            if (h1.ContainsKey(exaDV)) { bmp = h1[exaDV]; texture = exaDV; h = 1; }
                            else if (h1_gap.ContainsKey(exaDV)) { bmp = h1_gap[exaDV]; texture = exaDV; h = 1; modelType = 2; }
                            else if (h2.ContainsKey(exaDV)) { bmp = h2[exaDV]; texture = exaDV; h = 2; }
                            else if (h3.ContainsKey(exaDV)) { bmp = h3[exaDV]; texture = exaDV; h = 3; }
                            else if (h4_top.ContainsKey(exaDV)) { bmp = h4_top[exaDV]; texture = exaDV; h = 4; modelType = 1; }
                            else if (h4_bottom.ContainsKey(exaDV)) { bmp = h4_bottom[exaDV]; texture = exaDV; h = 4; }
                            else if (h5.ContainsKey(exaDV)) { bmp = h5[exaDV]; texture = exaDV; h = 5; }
                            else if (h6.ContainsKey(exaDV)) { bmp = h6[exaDV]; texture = exaDV; h = 6; }
                            else if (h7.ContainsKey(exaDV)) { bmp = h7[exaDV]; texture = exaDV; h = 7; }
                            else if (h8.ContainsKey(exaDV)) { bmp = h8[exaDV]; texture = exaDV; h = 8; }
                            else if (c1.ContainsKey(exaDV)) { bmp = c1[exaDV]; texture = exaDV; h = 8; modelType = 3; }
                            else if (c2.ContainsKey(exaDV)) { bmp = c2[exaDV]; texture = exaDV; h = 4; modelType = 4; }
                            else
                            {
                                if (h1.ContainsKey(dimDV)) { bmp = h1[dimDV]; texture = dimDV; h = 1; }
                                else if (h1_gap.ContainsKey(dimDV)) { bmp = h1_gap[dimDV]; texture = dimDV; h = 1; modelType = 2; }
                                else if (h2.ContainsKey(dimDV)) { bmp = h2[dimDV]; texture = dimDV; h = 2; }
                                else if (h3.ContainsKey(dimDV)) { bmp = h3[dimDV]; texture = dimDV; h = 3; }
                                else if (h4_top.ContainsKey(dimDV)) { bmp = h4_top[dimDV]; texture = dimDV; h = 4; modelType = 1; }
                                else if (h4_bottom.ContainsKey(dimDV)) { bmp = h4_bottom[dimDV]; texture = dimDV; h = 4; }
                                else if (h5.ContainsKey(dimDV)) { bmp = h5[dimDV]; texture = dimDV; h = 5; }
                                else if (h6.ContainsKey(dimDV)) { bmp = h6[dimDV]; texture = dimDV; h = 6; }
                                else if (h7.ContainsKey(dimDV)) { bmp = h7[dimDV]; texture = dimDV; h = 7; }
                                else if (h8.ContainsKey(dimDV)) { bmp = h8[dimDV]; texture = dimDV; h = 8; }
                                else if (c1.ContainsKey(dimDV)) { bmp = c1[dimDV]; texture = dimDV; h = 8; modelType = 3; }
                                else if (c2.ContainsKey(dimDV)) { bmp = c2[dimDV]; texture = dimDV; h = 4; modelType = 4; }
                                else continue;
                            }
                            

                            if (!usedTextures.Contains(texture.ToString()))
                                usedTextures.Add(texture.ToString());

                            #region random texture
                            var rnd = 0.0f;
                            bool textureError = true;
                            //if alpha = 0
                            while (textureError)
                            {
                                rnd = (float)random.NextDouble() * 16;
                                try
                                {
                                    Bitmap compare = new Bitmap(Environment.CurrentDirectory + "\\textures\\" + bmp.Replace("/", "\\") + ".png");
                                    textureError = (compare.GetPixel((int)rnd, (int)rnd).A <= 8);
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine("遇到了一个问题..请看崩溃报告");
                                    Console.WriteLine(bmp);
                                    File.WriteAllText(DateTime.Now.ToFileTime() + "-crash.txt", exception.ToString());
                                    Console.WriteLine(exception);
                                    if (args.Contains("nopause")) return;
                                    Console.WriteLine("按任意键继续...");
                                    Console.ReadKey(true);
                                    Environment.Exit(2);
                                }
                            }
                            if (args.Contains("smooth")) rnd = 0;
                            var face = new Model.Element.Face { texture = "#" + texture, uv = new[] { rnd, rnd, rnd, rnd } };
                            #endregion

                            //from to
                            var from = new[] { 0f, 0f, 0f };
                            var to = new[] { 0f, 0f, 0f };
                            if (modelType == 0 || modelType == 1 || modelType == 2)//layer
                            {
                                if (modelType == 0)//bottom
                                {
                                    from[0] = x; from[1] = y; from[2] = z;
                                    to[0] = x + 1; to[1] = y + h * 0.125f; to[2] = z + 1;
                                }
                                else if (modelType == 1)//top
                                {
                                    from[0] = x; from[1] = y + h * 0.125f; from[2] = z;
                                    to[0] = x + 1; to[1] = y + 1; to[2] = z + 1;
                                }
                                else if (modelType == 2)//gap
                                {
                                    from[0] = x + 0.1f; from[1] = y; from[2] = z + 0.1f;
                                    to[0] = x + 0.9f; to[1] = y + h * 0.125f; to[2] = z + 0.9f;
                                }
                            }
                            else if (modelType == 3 || modelType == 4)//connector
                            {
                                //x-,x+,z-,z+,y+
                                var banDV = new[] { "0_0", "0_0", "0_0", "0_0", "0_0" };
                                var bandimDV = new[] { "0_0", "0_0", "0_0", "0_0", "0_0" };
                                if (x - 1 >= 0) { banDV[0] = blocks[y * width * length + z * width + (x - 1)].ToString() + "_" + blockdata[y * width * length + z * width + (x - 1)].ToString(); bandimDV[0] = blocks[y * width * length + z * width + (x - 1)].ToString() + "_x"; }
                                if (x + 1 < width) { banDV[1] = blocks[y * width * length + z * width + (x + 1)].ToString() + "_" + blockdata[y * width * length + z * width + (x - 1)].ToString(); bandimDV[1] = blocks[y * width * length + z * width + (x + 1)].ToString() + "_x"; }
                                if (z - 1 >= 0) { banDV[2] = blocks[y * width * length + (z - 1) * width + x].ToString() + "_" + blockdata[y * width * length + (z - 1) * width + x].ToString(); bandimDV[2] = blocks[y * width * length + (z - 1) * width + x].ToString() + "_x"; }
                                if (z + 1 < length) { banDV[3] = blocks[y * width * length + (z + 1) * width + x].ToString() + "_" + blockdata[y * width * length + (z + 1) * width + x].ToString(); bandimDV[3] = blocks[y * width * length + (z + 1) * width + x].ToString() + "_x"; }
                                if (y + 1 < height) { banDV[4] = blocks[(y + 1) * width * length + z * width + x].ToString() + "_" + blockdata[(y + 1) * width * length + z * width + x].ToString(); bandimDV[4] = blocks[(y + 1) * width * length + z * width + x].ToString() + "_x"; }

                                if (modelType == 3)//glass_pane
                                {
                                    from[0] = x + 0.3f - ((!banConnect.ContainsKey(banDV[0]) && !banConnect.ContainsKey(bandimDV[0])) ? 0.3f : 0); from[1] = y; from[2] = z + 0.3f - ((!banConnect.ContainsKey(banDV[2]) && !banConnect.ContainsKey(bandimDV[2])) ? 0.3f : 0);
                                    to[0] = x + 0.7f + ((!banConnect.ContainsKey(banDV[1]) && !banConnect.ContainsKey(bandimDV[1])) ? 0.3f : 0); to[1] = y + 1; to[2] = z + 0.7f + ((!banConnect.ContainsKey(banDV[3]) && !banConnect.ContainsKey(bandimDV[3])) ? 0.3f : 0);
                                }
                                else if (modelType == 4)//fence & fence_gate
                                {
                                    from[0] = x + 0.3f - ((!banConnect.ContainsKey(banDV[0]) && !banConnect.ContainsKey(bandimDV[0])) ? 0.3f : 0); from[1] = y; from[2] = z + 0.3f - ((!banConnect.ContainsKey(banDV[2]) && !banConnect.ContainsKey(bandimDV[2])) ? 0.3f : 0);
                                    to[0] = x + 0.7f + ((!banConnect.ContainsKey(banDV[1]) && !banConnect.ContainsKey(bandimDV[1])) ? 0.3f : 0); to[1] = y + ((!banConnect.ContainsKey(banDV[4]) && !banConnect.ContainsKey(bandimDV[4])) ? 1 : 0.5f); to[2] = z + 0.7f + ((!banConnect.ContainsKey(banDV[3]) && !banConnect.ContainsKey(bandimDV[3])) ? 0.3f : 0);
                                }
                            }


                            //write face
                            elements.Add(new Model.Element
                            {
                                from = from,
                                to = to,
                                faces = new Dictionary<string, Model.Element.Face>
                                {
                                    {"North", face},
                                    {"East", face},
                                    {"South", face},
                                    {"West", face},
                                    {"Up", face},
                                    {"Down", face}
                                }
                            });
                        }
                    }
                }

                //remove unused
                foreach (var key in model.textures.Keys.ToList())
                {
                    if (!usedTextures.Contains(key)) model.textures.Remove(key);
                }

                // Scale it to 32 due to MC maximum
                if (!args.Contains("unlimit"))
                {
                    var max = elements.Select(element => element.to.Max()).Concat(new float[] { 0 }).Max();
                    var changedAmount = (max - 32.0f) / max;
                    if (changedAmount > 0)
                    {
                        Console.WriteLine("正在计算模型大小…");
                        foreach (var element in elements)
                        {
                            for (var i = 0; i < 3; i++)
                            {
                                element.from[i] = Math.Max(element.from[i] - changedAmount * element.from[i], 0);
                            }
                            for (var i = 0; i < 3; i++)
                            {
                                element.to[i] = Math.Max(element.to[i] - changedAmount * element.to[i], 0);
                            }
                        }
                    }
                }

                //return
                model.elements = elements.ToArray();
                Console.WriteLine("序列化中…");
                var extension = Path.GetExtension(file);
                var newFile = file.Replace(extension, ".json");
                File.WriteAllText(newFile, JsonConvert.SerializeObject(model, Formatting.Indented));
                Console.WriteLine("正在生成Json模型 " + newFile);
                if (args.Contains("nopause")) return;
                Console.WriteLine("按任意键继续...");
                Console.ReadKey(true);
            }
            catch (Exception exception)
            {
                Console.WriteLine("遇到了一个问题..请看崩溃报告");
                File.WriteAllText(DateTime.Now.ToFileTime() + "-crash.txt", exception.ToString());
                Console.WriteLine(exception);
                if (args.Contains("nopause")) return;
                Console.WriteLine("按任意键继续...");
                Console.ReadKey(true);
                Environment.Exit(2);
            }
        }
    }
}
