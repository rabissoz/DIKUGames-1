using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout;
/// <Summary>
/// Levels skal kunne aflæse Levels fra ASCII-filerne og
/// Skal kunne aflæse Legend og MetaData 
/// Ud fra aflæsnigen og vha. blocks kunne konvertere ASCII-filerne
/// indtil maps med blocks.
/// <param name="blockImg"> Billedet for blocken </param>
/// <param name="blockList"> Liste af blocks </param>
/// <param name="block"> en block </param>
/// </Summary>
    public class Levels{
        public EntityContainer<Block> blockList;
        private BlockImages blockImg;
        public Block block;
        public Dictionary<char, string> MetaDict;
        public Dictionary<char, string> LegendDict;
        private bool readingLegend = false;
        private Text display;
        public int legendStart;
        private int legendStop;
        private string[] legendArr;
        private int metaStart;
        private int metaStop;
        private string[] metaArr;
        private float rowCount = 0.9f;
        private float colOffset = 0.1f;
        private float colCount = 0.1f;

        public Levels() {
            blockList = new EntityContainer<Block>();
            blockImg = new BlockImages();
            MetaDict = new Dictionary<char, string>();
            LegendDict = new Dictionary<char, string>();
        }

        public void ReadLevel(int levelnum) {
            var fstPath = Path.Combine(Constants.MAIN_PATH, "Assets", "Assets", "Levels", "level" + levelnum + ".txt");
            string[] data = File.ReadAllLines(fstPath);

            legendStart = Array.FindIndex(data, i => i == ("Legend:")) + 1;
            legendStop = Array.FindIndex(data, i => i == ("Legend/")) - 1;
            metaStart = Array.FindIndex(data, i => i == ("Meta:")) + 1;
            metaStop = Array.FindIndex(data, i => i == ("Meta/")) - 1;
   
            ReadLegend(data);
            ReadMeta(data);
            }

        private void ReadMeta(string[] data) {
            for (int i = metaStart; i <= metaStop; i++) {
                string[] parts = data[i].Split(':');
                    if (parts.Length == 2)
                    {
                        char key = parts[1].Trim()[0];
                        string val = parts[0].Trim();

                        if (val == "Hardened" || val == "Unbreakable" )
                        {
                            if (!MetaDict.ContainsKey(key))
                            {
                                MetaDict.Add(key, val);
                            }
                        }
                    }
                if (parts[0].Trim() == "Name") {
                    display = new Text(parts[1].Trim().ToString(), new Vec2F(0.02f, 0.68f), new Vec2F(0.3f, 0.3f));
                }
           }
        }

        private void ReadLegend(string[] data) {
            for (int i = legendStart; i <= legendStop; i++) {
                        char key = data[i][0];
                        string val = data[i].Substring(3);
                    
                        if (!LegendDict.ContainsKey(key))
                        {
                            LegendDict.Add(key, val);
                        }   
            }
        }

        public void LvlCreation(int lvl) {
            ReadLevel(lvl);
            var fstPath = Path.Combine(Constants.MAIN_PATH, "Assets", "Assets","Levels", "level" + lvl + ".txt");
            string[] data = File.ReadAllLines(fstPath);
            for (int row = 1; row < data.Length; row++)
            {
                if (data[row] == "Map/")
                {
                    break;
                }
                rowCount -= 0.03f;
                colCount = -0.1f;
                for (int col = 1; col < 11; col++)
                {
                    colCount += colOffset;
                    if (data[row][col] == '-')
                    {
                        continue;
                    }
                    if (LegendDict.ContainsKey(data[row][col]))
                    {
                        BlockType type;
                        if (!MetaDict.ContainsKey(data[row][col])) {
                            type = BlockType.Normal;
                        }
                        else {
                            type = Enum.Parse<BlockType>(MetaDict[data[row][col]]);
                        }
                        block = blockImg.getBlockImg(colCount, rowCount, LegendDict[data[row][col]], type);
                        blockList.AddEntity(block);

                    }
                }
            }
            rowCount = 0.9f;
        }
    }
