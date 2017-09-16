using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
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

            Console.WriteLine("转换中...");

            try
            {
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
                Dictionary<string, string> all_block = new Dictionary<string, string>();
                all_block.Add("1_0", "blocks/stone_andesite");
                all_block.Add("1_1", "blocks/stone_granite");
                all_block.Add("1_2", "blocks/stone_granite_smooth");
                all_block.Add("1_3", "blocks/stone_diorite");
                all_block.Add("1_4", "blocks/stone_diorite_smooth");
                all_block.Add("1_5", "blocks/stone_andesite");
                all_block.Add("1_6", "blocks/stone_andesite_smooth");
                all_block.Add("2_0", "blocks/grass_path_top");
                all_block.Add("3_0", "blocks/dirt");
                all_block.Add("3_1", "blocks/coarse_dirt");
                all_block.Add("3_2", "blocks/dirt_podzol_top");
                all_block.Add("4_0", "blocks/cobblestone");
                all_block.Add("5_0", "blocks/planks_oak");
                all_block.Add("5_1", "blocks/planks_spruce");
                all_block.Add("5_2", "blocks/planks_birch");
                all_block.Add("5_3", "blocks/planks_jungle");
                all_block.Add("5_4", "blocks/planks_acacia");
                all_block.Add("5_5", "blocks/planks_big_oak");
                all_block.Add("7_0", "blocks/bedrock");
                all_block.Add("12_0", "blocks/sand");
                all_block.Add("12_1", "blocks/red_sand");
                all_block.Add("13_0", "blocks/gravel");
                all_block.Add("14_0", "blocks/gold_ore");
                all_block.Add("15_0", "blocks/iron_ore");
                all_block.Add("16_0", "blocks/coal_ore");
                all_block.Add("17_0", "blocks/log_oak");
                all_block.Add("17_1", "blocks/log_spruce");
                all_block.Add("17_2", "blocks/log_birch");
                all_block.Add("17_3", "blocks/log_jungle");
                all_block.Add("19_0", "blocks/sponge");
                all_block.Add("19_1", "blocks/sponge_wet");
                all_block.Add("20_0", "blocks/glass");
                all_block.Add("21_0", "blocks/lapis_ore");
                all_block.Add("22_0", "blocks/lapis_block");
                all_block.Add("23_0", "blocks/furnace_top");
                all_block.Add("24_0", "blocks/sandstone_normal");
                all_block.Add("24_1", "blocks/sandstone_carved");
                all_block.Add("24_2", "blocks/sandstone_smooth");
                all_block.Add("25_0", "blocks/noteblock");
                all_block.Add("29_0", "blocks/piston_top_sticky");
                all_block.Add("30_0", "blocks/web");
                all_block.Add("33_0", "blocks/piston_top_normal");
                all_block.Add("35_0", "blocks/wool_colored_white");
                all_block.Add("35_1", "blocks/wool_colored_orange");
                all_block.Add("35_2", "blocks/wool_colored_magenta");
                all_block.Add("35_3", "blocks/wool_colored_light_blue");
                all_block.Add("35_4", "blocks/wool_colored_yellow");
                all_block.Add("35_5", "blocks/wool_colored_lime");
                all_block.Add("35_6", "blocks/wool_colored_pink");
                all_block.Add("35_7", "blocks/wool_colored_gray");
                all_block.Add("35_8", "blocks/wool_colored_silver");
                all_block.Add("35_9", "blocks/wool_colored_cyan");
                all_block.Add("35_10", "blocks/wool_colored_purple");
                all_block.Add("35_11", "blocks/wool_colored_blue");
                all_block.Add("35_12", "blocks/wool_colored_brown");
                all_block.Add("35_13", "blocks/wool_colored_green");
                all_block.Add("35_14", "blocks/wool_colored_red");
                all_block.Add("35_15", "blocks/wool_colored_black");
                all_block.Add("41_0", "blocks/gold_block");
                all_block.Add("42_0", "blocks/iron_block");
                all_block.Add("43_0", "blocks/stone_slab_top");
                all_block.Add("43_1", "blocks/sandstone_top");
                all_block.Add("43_3", "blocks/cobblestone");
                all_block.Add("43_4", "blocks/brick");
                all_block.Add("43_5", "blocks/stonebrick");
                all_block.Add("43_6", "blocks/nether_brick");
                all_block.Add("43_7", "blocks/quartz_block_top");
                all_block.Add("125_0", "blocks/planks_oak");
                all_block.Add("125_1", "blocks/planks_spruce");
                all_block.Add("125_2", "blocks/planks_birch");
                all_block.Add("125_3", "blocks/planks_jungle");
                all_block.Add("125_4", "blocks/planks_acacia");
                all_block.Add("125_5", "blocks/planks_big_oak");
                all_block.Add("181_0", "blocks/red_sandstone_top");
                all_block.Add("204_0", "blocks/purpur_block");
                all_block.Add("45_0", "blocks/brick");
                all_block.Add("46_0", "blocks/tnt_side");
                all_block.Add("47_0", "blocks/bookshelf");
                all_block.Add("48_0", "blocks/cobblestone_mossy");
                all_block.Add("49_0", "blocks/obsidian");
                all_block.Add("53_0", "blocks/planks_oak");
                all_block.Add("56_0", "blocks/diamond_ore");
                all_block.Add("57_0", "blocks/diamond_block");
                all_block.Add("58_0", "blocks/crafting_table_top");
                all_block.Add("60_0", "blocks/farmland_dry");
                all_block.Add("61_0", "blocks/furnace_top");
                all_block.Add("67_0", "blocks/cobblestone");
                all_block.Add("73_0", "blocks/redstone_ore");
                all_block.Add("78_0", "blocks/snow");
                all_block.Add("79_0", "blocks/ice");
                all_block.Add("80_0", "blocks/snow");
                all_block.Add("81_0", "blocks/cactus_side");
                all_block.Add("82_0", "blocks/clay");
                all_block.Add("84_0", "blocks/jukebox_side");
                all_block.Add("86_0", "blocks/pumpkin_side");
                all_block.Add("87_0", "blocks/netherrack");
                all_block.Add("88_0", "blocks/soul_sand");
                all_block.Add("89_0", "blocks/glowstone");
                all_block.Add("91_0", "blocks/pumpkin_side");
                all_block.Add("95_0", "blocks/glass_white");
                all_block.Add("95_1", "blocks/glass_orange");
                all_block.Add("95_2", "blocks/glass_magenta");
                all_block.Add("95_3", "blocks/glass_light_blue");
                all_block.Add("95_4", "blocks/glass_yellow");
                all_block.Add("95_5", "blocks/glass_lime");
                all_block.Add("95_6", "blocks/glass_pink");
                all_block.Add("95_7", "blocks/glass_gray");
                all_block.Add("95_8", "blocks/glass_light");
                all_block.Add("95_9", "blocks/glass_cyan");
                all_block.Add("95_10", "blocks/glass_purple");
                all_block.Add("95_11", "blocks/glass_blue");
                all_block.Add("95_12", "blocks/glass_brown");
                all_block.Add("95_13", "blocks/glass_green");
                all_block.Add("95_14", "blocks/glass_red");
                all_block.Add("95_15", "blocks/glass_black");
                all_block.Add("97_0", "blocks/stone");
                all_block.Add("97_1", "blocks/cobblestone");
                all_block.Add("97_2", "blocks/stonebrick");
                all_block.Add("97_3", "blocks/stonebrick_mossy");
                all_block.Add("97_4", "blocks/stonebrick_cracked");
                all_block.Add("97_5", "blocks/stonebrick_carved");
                all_block.Add("98_0", "blocks/stonebrick");
                all_block.Add("98_1", "blocks/stonebrick_mossy");
                all_block.Add("98_2", "blocks/stonebrick_cracked");
                all_block.Add("98_3", "blocks/stonebrick_carved");
                all_block.Add("99_0", "blocks/mushroom_block_skin_brown");
                all_block.Add("100_0", "blocks/mushroom_block_skin_red");
                all_block.Add("102_0", "blocks/glass");
                all_block.Add("103_0", "blocks/melon_side");
                all_block.Add("108_0", "blocks/brick");
                all_block.Add("109_0", "blocks/stonebrick");
                all_block.Add("110_0", "blocks/mycelium_top");
                all_block.Add("112_0", "blocks/nether_brick");
                all_block.Add("114_0", "blocks/nether_brick");
                all_block.Add("120_0", "blocks/endframe_top");
                all_block.Add("121_0", "blocks/end_stone");
                all_block.Add("122_0", "blocks/dragon_egg");
                all_block.Add("123_0", "blocks/redstone_lamp_on");
                all_block.Add("124_0", "blocks/redstone_lamp_off");
                all_block.Add("128_0", "blocks/sandstone_normal");
                all_block.Add("129_0", "blocks/emerald_ore");
                all_block.Add("133_0", "blocks/emerald_block");
                all_block.Add("134_0", "blocks/planks_spruce");
                all_block.Add("135_0", "blocks/planks_birch");
                all_block.Add("136_0", "blocks/planks_jungle");
                all_block.Add("137_0", "blocks/command_block_front");
                all_block.Add("138_0", "blocks/beacon");
                all_block.Add("139_0", "blocks/cobblestone");
                all_block.Add("139_1", "blocks/cobblestone_mossy");
                all_block.Add("145_0", "blocks/anvil_top_damaged_0");
                all_block.Add("145_1", "blocks/anvil_top_damaged_1");
                all_block.Add("145_2", "blocks/anvil_top_damaged_2");
                all_block.Add("152_0", "blocks/redstone_block");
                all_block.Add("153_0", "blocks/quartz_ore");
                all_block.Add("154_0", "blocks/hopper_top");
                all_block.Add("155_0", "blocks/quartz_block_side");
                all_block.Add("155_1", "blocks/quartz_block_chiseled");
                all_block.Add("155_2", "blocks/quartz_block_lines");
                all_block.Add("156_0", "blocks/quartz_block");
                all_block.Add("158_0", "blocks/furnace_side");
                all_block.Add("159_0", "blocks/hardened_clay_stained_white");
                all_block.Add("159_1", "blocks/hardened_clay_stained_orange");
                all_block.Add("159_2", "blocks/hardened_clay_stained_magenta");
                all_block.Add("159_3", "blocks/hardened_clay_stained_light_blue");
                all_block.Add("159_4", "blocks/hardened_clay_stained_yellow");
                all_block.Add("159_5", "blocks/hardened_clay_stained_lime");
                all_block.Add("159_6", "blocks/hardened_clay_stained_pink");
                all_block.Add("159_7", "blocks/hardened_clay_stained_gray");
                all_block.Add("159_8", "blocks/hardened_clay_stained_silver");
                all_block.Add("159_9", "blocks/hardened_clay_stained_cyan");
                all_block.Add("159_10", "blocks/hardened_clay_stained_purple");
                all_block.Add("159_11", "blocks/hardened_clay_stained_blue");
                all_block.Add("159_12", "blocks/hardened_clay_stained_brown");
                all_block.Add("159_13", "blocks/hardened_clay_stained_green");
                all_block.Add("159_14", "blocks/hardened_clay_stained_red");
                all_block.Add("159_15", "blocks/hardened_clay_stained_black");
                all_block.Add("160_0", "blocks/glass_white");
                all_block.Add("160_1", "blocks/glass_orange");
                all_block.Add("160_2", "blocks/glass_magenta");
                all_block.Add("160_3", "blocks/glass_light_blue");
                all_block.Add("160_4", "blocks/glass_yellow");
                all_block.Add("160_5", "blocks/glass_lime");
                all_block.Add("160_6", "blocks/glass_pink");
                all_block.Add("160_7", "blocks/glass_gray");
                all_block.Add("160_8", "blocks/glass_silver");
                all_block.Add("160_9", "blocks/glass_cyan");
                all_block.Add("160_10", "blocks/glass_purple");
                all_block.Add("160_11", "blocks/glass_blue");
                all_block.Add("160_12", "blocks/glass_brown");
                all_block.Add("160_13", "blocks/glass_green");
                all_block.Add("160_14", "blocks/glass_red");
                all_block.Add("160_15", "blocks/glass_black");
                all_block.Add("162_0", "blocks/log_acacia");
                all_block.Add("162_1", "blocks/log_big_oak");
                all_block.Add("163_0", "blocks/planks_acacia");
                all_block.Add("164_0", "blocks/planks_big_oak");
                all_block.Add("165_0", "blocks/slime");
                all_block.Add("168_0", "blocks/prismarine_rough");
                all_block.Add("168_1", "blocks/prismarine_bricks");
                all_block.Add("168_2", "blocks/prismarine_dark");
                all_block.Add("169_0", "blocks/sea_lantern");
                all_block.Add("170_0", "blocks/hay_block_side");
                all_block.Add("172_0", "blocks/hardened_clay");
                all_block.Add("173_0", "blocks/coal_block");
                all_block.Add("174_0", "blocks/ice_packed");
                all_block.Add("179_0", "blocks/red_sandstone_normal");
                all_block.Add("179_1", "blocks/red_sandstone_carved");
                all_block.Add("179_2", "blocks/red_sandstone_smooth");
                all_block.Add("180_0", "blocks/red_sandstone_normal");
                all_block.Add("199_0", "blocks/chorus_plant");
                all_block.Add("200_0", "blocks/chorus_flower");
                all_block.Add("201_0", "blocks/purpur_block");
                all_block.Add("202_0", "blocks/purpur_pillar");
                all_block.Add("203_0", "blocks/purpur_block");
                all_block.Add("206_0", "blocks/end_bricks");
                all_block.Add("213_0", "blocks/magma");
                all_block.Add("214_0", "blocks/nether_wart_block");
                all_block.Add("215_0", "blocks/red_nether_brick");
                all_block.Add("216_0", "blocks/bone_block_side");
                all_block.Add("218_0", "blocks/furnace_top");
                all_block.Add("219_0", "blocks/shulker_top_white");
                all_block.Add("220_0", "blocks/shulker_top_orange");
                all_block.Add("221_0", "blocks/shulker_top_magenta");
                all_block.Add("222_0", "blocks/shulker_top_light_blue");
                all_block.Add("223_0", "blocks/shulker_top_yellow");
                all_block.Add("224_0", "blocks/shulker_top_lime");
                all_block.Add("225_0", "blocks/shulker_top_pink");
                all_block.Add("226_0", "blocks/shulker_top_gray");
                all_block.Add("227_0", "blocks/shulker_top_silver");
                all_block.Add("228_0", "blocks/shulker_top_cyan");
                all_block.Add("229_0", "blocks/shulker_top_purple");
                all_block.Add("230_0", "blocks/shulker_top_blue");
                all_block.Add("231_0", "blocks/shulker_top_brown");
                all_block.Add("232_0", "blocks/shulker_top_green");
                all_block.Add("233_0", "blocks/shulker_top_red");
                all_block.Add("234_0", "blocks/shulker_top_black");
                all_block.Add("235_0", "blocks/glazed_terracotta_white");
                all_block.Add("236_0", "blocks/glazed_terracotta_orange");
                all_block.Add("237_0", "blocks/glazed_terracotta_magenta");
                all_block.Add("238_0", "blocks/glazed_terracotta_light_blue");
                all_block.Add("239_0", "blocks/glazed_terracotta_yellow");
                all_block.Add("240_0", "blocks/glazed_terracotta_lime");
                all_block.Add("241_0", "blocks/glazed_terracotta_pink");
                all_block.Add("242_0", "blocks/glazed_terracotta_gray");
                all_block.Add("243_0", "blocks/glazed_terracotta_silver");
                all_block.Add("244_0", "blocks/glazed_terracotta_cyan");
                all_block.Add("245_0", "blocks/glazed_terracotta_purple");
                all_block.Add("246_0", "blocks/glazed_terracotta_blue");
                all_block.Add("247_0", "blocks/glazed_terracotta_brown");
                all_block.Add("248_0", "blocks/glazed_terracotta_green");
                all_block.Add("249_0", "blocks/glazed_terracotta_red");
                all_block.Add("250_0", "blocks/glazed_terracotta_black");
                all_block.Add("251_0", "blocks/concrete_white");
                all_block.Add("251_1", "blocks/concrete_orange");
                all_block.Add("251_2", "blocks/concrete_magenta");
                all_block.Add("251_3", "blocks/concrete_light_blue");
                all_block.Add("251_4", "blocks/concrete_yellow");
                all_block.Add("251_5", "blocks/concrete_lime");
                all_block.Add("251_6", "blocks/concrete_pink");
                all_block.Add("251_7", "blocks/concrete_gray");
                all_block.Add("251_8", "blocks/concrete_silver");
                all_block.Add("251_9", "blocks/concrete_cyan");
                all_block.Add("251_10", "blocks/concrete_purple");
                all_block.Add("251_11", "blocks/concrete_blue");
                all_block.Add("251_12", "blocks/concrete_brown");
                all_block.Add("251_13", "blocks/concrete_green");
                all_block.Add("251_14", "blocks/concrete_red");
                all_block.Add("251_15", "blocks/concrete_black");
                all_block.Add("252_0", "blocks/concrete_powder_white");
                all_block.Add("252_1", "blocks/concrete_powder_orange");
                all_block.Add("252_2", "blocks/concrete_powder_magenta");
                all_block.Add("252_3", "blocks/concrete_powder_light_blue");
                all_block.Add("252_4", "blocks/concrete_powder_yellow");
                all_block.Add("252_5", "blocks/concrete_powder_lime");
                all_block.Add("252_6", "blocks/concrete_powder_pink");
                all_block.Add("252_7", "blocks/concrete_powder_gray");
                all_block.Add("252_8", "blocks/concrete_powder_silver");
                all_block.Add("252_9", "blocks/concrete_powder_cyan");
                all_block.Add("252_10", "blocks/concrete_powder_purple");
                all_block.Add("252_11", "blocks/concrete_powder_blue");
                all_block.Add("252_12", "blocks/concrete_powder_brown");
                all_block.Add("252_13", "blocks/concrete_powder_green");
                all_block.Add("252_14", "blocks/concrete_powder_red");
                all_block.Add("252_15", "blocks/concrete_powder_black");
                all_block.Add("255_0", "blocks/structure_block");
                    //water
                all_block.Add("8_0", "blocks/water_flow");
                all_block.Add("8_5", "blocks/water_flow");
                all_block.Add("8_6", "blocks/water_flow");
                all_block.Add("8_7", "blocks/water_flow");
                all_block.Add("9_0", "blocks/water_still");
                all_block.Add("9_5", "blocks/water_still");
                all_block.Add("9_6", "blocks/water_still");
                all_block.Add("9_7", "blocks/water_still");
                    //lava
                all_block.Add("10_0", "blocks/lava_flow");
                all_block.Add("10_5", "blocks/lava_flow");
                all_block.Add("10_6", "blocks/lava_flow");
                all_block.Add("10_7", "blocks/lava_flow");
                all_block.Add("11_0", "blocks/lava_still");
                all_block.Add("11_5", "blocks/lava_still");
                all_block.Add("11_6", "blocks/lava_still");
                all_block.Add("11_7", "blocks/lava_still");

                //define slab
                Dictionary<string, string> slab = new Dictionary<string, string>();
                slab.Add("44_0", "blocks/stone_slab_top");
                slab.Add("44_1", "blocks/sandstone_top");
                slab.Add("44_3", "blocks/cobblestone");
                slab.Add("44_4", "blocks/brick");
                slab.Add("44_5", "blocks/stonebrick");
                slab.Add("44_6", "blocks/nether_brick");
                slab.Add("44_7", "blocks/quartz_block_top");
                slab.Add("126_0", "blocks/planks_oak");
                slab.Add("126_1", "blocks/planks_spruce");
                slab.Add("126_2", "blocks/planks_birch");
                slab.Add("126_3", "blocks/planks_jungle");
                slab.Add("126_4", "blocks/planks_acacia");
                slab.Add("126_5", "blocks/planks_big_oak");
                slab.Add("182_0", "blocks/red_sandstone_top");
                slab.Add("205_0", "blocks/purpur_block");
                    //water
                slab.Add("8_1", "blocks/water_flow");
                slab.Add("8_2", "blocks/water_flow");
                slab.Add("8_3", "blocks/water_flow");
                slab.Add("8_4", "blocks/water_flow");
                slab.Add("9_1", "blocks/water_still");
                slab.Add("9_2", "blocks/water_still");
                slab.Add("9_3", "blocks/water_still");
                slab.Add("9_4", "blocks/water_still");
                    //lava
                slab.Add("10_1", "blocks/lava_flow");
                slab.Add("10_2", "blocks/lava_flow");
                slab.Add("10_3", "blocks/lava_flow");
                slab.Add("10_4", "blocks/lava_flow");
                slab.Add("11_1", "blocks/lava_still");
                slab.Add("11_2", "blocks/lava_still");
                slab.Add("11_3", "blocks/lava_still");
                slab.Add("11_4", "blocks/lava_still");
                Dictionary<string, string> slab_top = new Dictionary<string, string>();
                slab_top.Add("44_8", "blocks/stone_slab_top");
                slab_top.Add("44_9", "blocks/sandstone_top");
                slab_top.Add("44_10", "blocks/planks_oak");
                slab_top.Add("44_11", "blocks/cobblestone");
                slab_top.Add("44_12", "blocks/brick");
                slab_top.Add("44_13", "blocks/stonebrick");
                slab_top.Add("44_14", "blocks/nether_brick");
                slab_top.Add("44_15", "blocks/quartz_block_top");
                slab_top.Add("126_8", "blocks/planks_oak");
                slab_top.Add("126_9", "blocks/planks_spruce");
                slab_top.Add("126_10", "blocks/planks_birch");
                slab_top.Add("126_11", "blocks/planks_jungle");
                slab_top.Add("126_12", "blocks/planks_acacia");
                slab_top.Add("126_13", "blocks/planks_big_oak");
                slab_top.Add("182_8", "blocks/red_sandstone_top");
                slab_top.Add("205_8", "blocks/purpur_block");

                //define slab upper
                foreach (var item in all_block)
                {
                    model.textures.Add(item.Key, item.Value);
                }
                foreach (var item in slab)
                {
                    model.textures.Add(item.Key, item.Value);
                }
                foreach (var item in slab_top)
                {
                    model.textures.Add(item.Key, item.Value);
                }

                var elements = new List<Model.Element>();
                var random = new Random();
                var usedTextures = new List<string>();

                Console.WriteLine("width = " + width + " ,height = " + height + " ,length = " + length);

                for (var x = 0; x < width; ++x)
                {
                    for (var y = 0; y < height; ++y)
                    {
                        for (var z = 0; z < length; ++z)
                        {
                            var index = y * width * length + z * width + x;
                            var block = blocks[index];
                            var data = blockdata[index];
                            string texture = "";
                            var islab = false;
                            var top = false;
                            //*define

                            string m = block.ToString() + "_" + data.ToString();
                            if (all_block.ContainsKey(m))
                            {
                                texture = m;
                                islab = false;

                            }//allblock
                            else if (slab.ContainsKey(m))
                            {
                                texture = m;
                                islab = true;
                                top = false;
                            }//slab_bottom
                            else if (slab_top.ContainsKey(m))
                            {
                                texture = m;
                                islab = true;
                                top = true;
                            }//slab_upper
                            else
                                continue;

                            if (!usedTextures.Contains(texture.ToString()))
                                usedTextures.Add(texture.ToString());

                            //random texture
                            var rnd = (float)random.NextDouble() * 16;
                            if (args.Contains("smooth")) rnd = 0;
                            var face = new Model.Element.Face { texture = "#" + texture, uv = new[] { rnd, rnd, rnd, rnd } };
                            //write face
                            elements.Add(new Model.Element
                            {
                                from = new[] { x, y + (islab && top ? 0.5f : 0), z },
                                to = new[] { x + 1, y + (islab && !top ? 0.5f : 1), z + 1 },
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
                Console.WriteLine("正在将Json转化为文件 " + newFile);
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
