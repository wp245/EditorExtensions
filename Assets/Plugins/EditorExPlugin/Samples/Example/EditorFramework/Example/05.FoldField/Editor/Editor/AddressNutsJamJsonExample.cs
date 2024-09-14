using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using NutsJamEditorEx;

namespace EditorFramework
{
    [CustomEditorWindow(6)]
    public class AddressNutsJamJsonExample: EditorWindow
    {
        private string mReadJsonPath = "";
        private GameLevelConfig mGameLevelConfig = new GameLevelConfig();
        private string mGenLevelCount = "";
        private string mLayersPerLevel = "";
        private string mNewLevelPre = "";

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Label("单层板子池配置json文件");
                GUILayout.BeginHorizontal();
                {
                    var currentGUIEnable = GUI.enabled;
                    GUI.enabled = false;
                    GUILayout.TextField(mReadJsonPath);
                    GUI.enabled = currentGUIEnable;

                    if (GUILayout.Button("选择文件"))
                    {
                        mReadJsonPath = EditorUtility.OpenFilePanel("选择文件", "", "json");
                    }
                    // 打开文件所在目录
                    if (GUILayout.Button("打开文件所在目录"))
                    {
                        EditorUtility.RevealInFinder(mReadJsonPath);
                    }
                }
                GUILayout.EndHorizontal();
                
                GUILayout.Space(100);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("生成关卡数量");
                    mGenLevelCount = GUILayout.TextField(mGenLevelCount);
                    GUILayout.Label("每关层数");
                    mLayersPerLevel = GUILayout.TextField(mLayersPerLevel);
                    GUILayout.Label("关卡基数");
                    mNewLevelPre = GUILayout.TextField(mNewLevelPre);
                    if (GUILayout.Button("读取json文件并生成关卡json"))
                    {
                        string text = File.ReadAllText(mReadJsonPath);
                        mGameLevelConfig = JsonUtility.FromJson<GameLevelConfig>(text);
                        var levelDSingleBoardCount = mGameLevelConfig.data.Count;
                        var configData = mGameLevelConfig.data;
                        var genLevelCount = int.Parse(mGenLevelCount);
                        var layersPerLevel = int.Parse(mLayersPerLevel);
                        var newLevelPre = int.Parse(mNewLevelPre);
                        GameLevelConfig newGameLevelConfig = new GameLevelConfig();
                        for (int i = 1; i <= genLevelCount; i++)
                        {
                            var tempGameChildLevelData = new GameChildLevelData()
                            {
                                totalLayer = 0,
                                freeHoleCount = 5,
                                haveTimeLimit = false,
                                limitTime = 0,
                                allUnderNailData = new List<GameNailData>(),
                                allGameBoard = new List<GameBoardData>(),
                                gameNailBoxData = new List<GameNailBoxData>(),
                            };
                            for (int j = 1; j < layersPerLevel; j++)
                            {
                                int randomIndex = Random.Range(1, levelDSingleBoardCount+1);
                                
                                foreach (var gameChildLevelData in configData)
                                {
                                   if(int.Parse(gameChildLevelData.Key)%10000 == randomIndex)
                                   {
                                       gameChildLevelData.Value.allGameBoard[0].belongLayer = j;
                                       tempGameChildLevelData.allGameBoard.Add(gameChildLevelData.Value.allGameBoard[0]);
                                   }
                                }
                            }
                            newGameLevelConfig.data.Add((newLevelPre+i).ToString(), tempGameChildLevelData);
                        }
                        // 获取文件所在文件夹的路径
                        var folderPath = Path.GetDirectoryName(mReadJsonPath);
                        // 获取文件名
                        var fileName = "new_new_new_" + Path.GetFileName(mReadJsonPath);
                        // 生成新的json文件
                        File.WriteAllText(Path.Combine(folderPath, "new_" + fileName), JsonUtility.ToJson(newGameLevelConfig));
                        
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
    }
}


// {
//     "version": "1.0.2",
//     "data": {
//         "10000": {
//             "totalLayer": 0,
//             "freeHoleCount": 5,
//             "haveTimeLimit": false,
//             "limitTime": 0,
//             "allUnderNailData": [],
//             "allGameBoard": [],
//             "gameNailBoxData": [
//                 {
//                     "colorIndex": 8,
//                     "allNailHole": [
//                         0,
//                         0,
//                         0
//                     ]
//                 }
//             ]
//         }
//     }
// }