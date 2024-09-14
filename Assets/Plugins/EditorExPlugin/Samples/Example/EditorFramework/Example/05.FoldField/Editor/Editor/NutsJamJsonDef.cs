using System.Collections.Generic;

namespace NutsJamEditorEx
{
    /// <summary>
    /// 游戏板子预设表
    /// </summary>
    public class GameBoardPrefabConfig
    {
        public List<GameBoardData> gameBoardData = new List<GameBoardData>();
    }

    /// <summary>
    /// 板子谜题关卡数据
    /// </summary>
    public class GameLevelConfig
    {
        public string version = "1.0.0";
        public Dictionary<string, GameChildLevelData> data = new Dictionary<string, GameChildLevelData>();
    }

    /// <summary>
    /// 板子谜题子关卡数据
    /// </summary>
    public class GameChildLevelData
    {
        /// <summary>
        /// 总共有多少个层级
        /// </summary>
        public int totalLayer;

        /// <summary>
        /// 免费的孔的数量
        /// </summary>
        public int freeHoleCount = 5;

        /// <summary>
        /// 是否是限时关卡
        /// </summary>
        public bool haveTimeLimit;

        /// <summary>
        /// 限制时间
        /// </summary>
        public int limitTime;

        /// <summary>
        /// 所有的钉子信息，和 所有 底板螺丝孔对应
        /// </summary>
        public List<GameNailData> allUnderNailData = new List<GameNailData>();

        /// <summary>
        /// 棋盘上的所有板子
        /// </summary>
        public List<GameBoardData> allGameBoard = new List<GameBoardData>();

        /// <summary>
        /// 所有螺丝盒的信息
        /// </summary>
        public List<GameNailBoxData> gameNailBoxData = new List<GameNailBoxData>();
    }

    /// <summary>
    /// 板子谜题棋盘孔洞数据
    /// </summary>
    public class ChessBoardHoleData
    {
        /// <summary>
        /// 所处的位置坐标
        /// </summary>
        public BoardVector pos;

        /// <summary>
        /// 是否需要解锁
        /// </summary>
        public bool needUnlock;

        public EditHoleType defaultHoleData;
    }

    /// <summary>
    /// 游戏板子数据
    /// </summary>
    public class GameBoardData
    {
        /// <summary>
        /// 板子的名字
        /// </summary>
        public string boardId;

        /// <summary>
        /// 所属的层级
        /// </summary>
        public int belongLayer;

        /// <summary>
        /// 所处的位置坐标
        /// </summary>
        public BoardVector pos;

        /// <summary>
        /// 旋转角度
        /// </summary>
        public float rotation;

        /// <summary>
        /// 缩放信息
        /// </summary>
        public BoardVector scale;

        /// <summary>
        /// 所有的螺丝的信息，与上方位置信息一一对应
        /// </summary>
        public List<GameNailData> allNailData = new List<GameNailData>();

        /// <summary>
        /// 重心
        /// </summary>
        public BoardVector centerOfMess = new BoardVector();

        /// <summary>
        /// 板子的颜色
        /// </summary>
        public BoardVector boardColor = new BoardVector();
    }

    /// <summary>
    /// 游戏螺丝信息
    /// </summary>
    public class GameNailData
    {
        public string nailId;
        public int colorIndex;
        public NailType nailType;
        public NailExternalType nailExternalType;
        public bool shiftOpenState;
        public int boomRound;

        /// <summary>
        /// 所处的位置坐标
        /// </summary>
        public BoardVector localPos;

        /// <summary>
        /// 所处的层级下标
        /// </summary>
        public int layerIndex;

        public List<string> bindNailId;
    }

    /// <summary>
    /// 螺丝盒的数据
    /// </summary>
    public class GameNailBoxData
    {
        /// <summary>
        /// 颜色下表，默认 0
        /// </summary>
        public int colorIndex = 0;

        /// <summary>
        /// 默认三个方孔
        /// </summary>
        public List<NailType> allNailHole = new List<NailType>();
    }

    /// <summary>
    /// 自定义的板子点
    /// </summary>
    public class BoardVector
    {
        public float x;
        public float y;
        public float z = 1;
        public float w = 1;

        public BoardVector(float x = 0, float y = 0, float z = 1, float w = 1)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    public class GameNailInLevelLocationData
    {
        /// <summary>
        /// 在数据中的下标
        /// </summary>
        public int index;

        /// <summary>
        /// 父亲木板在数据中的下标
        /// </summary>
        public int fatherBoardIndex = -1;

        public GameNailInLevelLocationData(int index, int fatherBoardIndex = -1)
        {
            this.index = index;
            this.fatherBoardIndex = fatherBoardIndex;
        }
    }

    /// <summary>
    /// 编辑螺丝钉类型
    /// </summary>
    public enum NailType
    {
        Cross, // 十字
        Triangle, // 三角
        Quadrangle, // 四角
        Hexagonal, // 六角
    }

    /// <summary>
    /// 外部情况类型
    /// </summary>
    public enum NailExternalType
    {
        None = 0,
        Frozen,
        Connected,
        Shift,
        Chained,
        Boom,
        Key,
        Lock
    }

    /// <summary>
    /// 编辑器下螺丝孔的三种类型
    /// </summary>
    public enum EditHoleType
    {
        Empty,
        WithNail,
        NeedUnlock,
    }

    /// <summary>
    /// 游戏模式
    /// </summary>
    public enum GameMode
    {
        EditMode = -1,
        Normal = 0,
        Challenge = 1,
    }
}