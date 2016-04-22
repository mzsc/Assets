using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class PlaneWarControl : MonoBehaviour
{
	public GameObject[] Enemy;//敌机种类数组
	public GameObject[] Prop;//弹药种类数组
	public bool BoolGameOver = false;//游戏结束变量
	public bool BoolPause = false;//游戏暂停变量

	private GameObject MyBomNumber;//UGUI弹药数显示
	private GameObject MyScore;//UGUI分数显示
	private GameObject MyQuit;//UGUI退出按钮
	private GameObject MyRestart;//UGUI重新开始按钮
    private RectTransform RTGameOver;
	private Text BomNumberText;//UGUI弹药数显示文言
	private Text ScoreText;//UGUI分数显示文言
    private Text ScoreMax;
    private Text ScoreNow;
	private int IntBomNumber = 0;//弹药数
	private int IntScore = 0;//分数
    private float StartTime;
	
	void Awake ()
	{
		Time.fixedDeltaTime = 0.2f;//设置FixedUpdate的更新间隔为1S
		Time.timeScale = 1f;//时间缩放为1
		//获取显示弹药数量的物体
		MyBomNumber = GameObject.Find ("/Canvas/Bomb/Text");
		//获取显示弹药数量的Text组件
		BomNumberText = MyBomNumber.GetComponent <Text> ();
		//初始化显示数量为0
		BomNumberText.text = "x" + IntBomNumber.ToString ();
		//获取显示分数的物体
		MyScore = GameObject.Find ("/Canvas/Score");
		//获取显示分数的Text组件
		ScoreText = MyScore.GetComponent <Text> ();
		//初始化显示分数为0
		ScoreText.text = "分数：" + IntScore.ToString ();
        RTGameOver = GameObject.Find("/Canvas/GameOver").GetComponent<RectTransform>();
        ScoreMax = GameObject.Find("/Canvas/GameOver/ScoreMax/Text").GetComponent<Text>();
        ScoreNow = GameObject.Find("/Canvas/GameOver/ScoreNow/Text").GetComponent<Text>();
		BoolGameOver = false;//游戏结束为假
        BoolPause = true;//游戏暂停为假
	}
        
	void Start ()
	{
		//每隔3S运行一次InstantiateEnemy0()函数
		InvokeRepeating ("InstantiateEnemy0", 4f, 3f);
		//每隔8S运行一次InstantiateEnemy1()函数
		InvokeRepeating ("InstantiateEnemy1", 5f, 8f);
		//每隔15S运行一次InstantiateEnemy2()函数
		InvokeRepeating ("InstantiateEnemy2", 6f, 15f);
		//每隔10S运行一次InstantiateProp()函数
		InvokeRepeating ("InstantiateProp", 8f, 10f);
	}

	void Update ()
	{
		//如果游戏结束，否则判断是否按下空格键
        if (BoolGameOver && (RTGameOver.anchoredPosition.y > 0)) {
            if (RTGameOver.anchoredPosition.y > 1200) {
                MyScore.SetActive(false);
                GameOver();
            }
            RTGameOver.anchoredPosition = new Vector2 (0,Mathf.Lerp(1200,0,(Time.time - StartTime) / 2));
            if (RTGameOver.anchoredPosition.y < 1) {
                RTGameOver.anchoredPosition = new Vector2(0, 0);
            }
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			BoolPause = !BoolPause;//暂停、开始互换
        }else {
            StartTime = Time.time;
        }
	}

	//敌机0生成函数
	void InstantiateEnemy0 ()
	{
		//如果非结束非暂停
		if ((!BoolGameOver) && (!BoolPause)) {
			float x = Random.Range (-35f, 35f);//随机在5~65返回横坐标
			//克隆敌机0在指定位置
			Instantiate (Enemy [0], new Vector3 (x, 80, 90), new Quaternion (0, 0, 0, 0));
		}
	}

	//敌机1生成函数
	void InstantiateEnemy1 ()
	{
		//如果非结束非暂停
		if ((!BoolGameOver) && (!BoolPause)) {
			float x = Random.Range (-35f, 35f);//随机在5~65返回横坐标
			//克隆敌机1在指定位置
			Instantiate (Enemy [1], new Vector3 (x, 100, 90), new Quaternion (0, 0, 0, 0));
		}
	}

	//敌机2生成函数
	void InstantiateEnemy2 ()
	{
		//如果非结束非暂停
		if ((!BoolGameOver) && (!BoolPause)) {
			float x = Random.Range (-25f, 25f);//随机在15~55返回横坐标
			//克隆敌机2在指定位置
			Instantiate (Enemy [2], new Vector3 (x, 140, 90), new Quaternion (0, 0, 0, 0));
		}
	}

	//弹药生成函数
	void InstantiateProp ()
	{
		//如果非结束非暂停
		if ((!BoolGameOver) && (!BoolPause)) {
			float x = Random.Range (-35f, 35f);//随机在5~65返回横坐标
			int i = Random.Range (0, 100);//随机返回0~100的数
			//如果i大于50则生成弹药1，否则弹药0
			if (i > 50) {
				i = 1;
			} else {
				i = 0;
			}
			//克隆弹药在指定位置
			Instantiate (Prop [i], new Vector3 (x, 180, 90), new Quaternion (0, 0, 0, 0));
		}
	}

	//弹药数量显示函数
	void ChangeBomNumber (int Num)
	{
		IntBomNumber += Num;//弹药数量加Num
		BomNumberText.text = "x" + IntBomNumber.ToString ();//更新显示的弹药数
	}

	//分数显示函数
	void ChangeScore (int Num)
	{
		IntScore += Num;//分数加Num
		ScoreText.text = "分数：" + IntScore.ToString ();//更新显示的分数
	}

    void GameOver(){
        string Path = "./ScoreMax.dat";
        if (!File.Exists(Path)) {
            FileStream FS = new FileStream(Path, FileMode.Create);
            BinaryWriter BW = new BinaryWriter(FS);
            BW.Write(0);
            BW.Close();
            FS.Close();
        }
        FileStream FRScoreMax = new FileStream(Path, FileMode.Open, FileAccess.Read);
        BinaryReader RScoreMax = new BinaryReader(FRScoreMax);
        int IScoreMax = RScoreMax.ReadInt32();
        RScoreMax.Close();
        FRScoreMax.Close();
        if (IScoreMax < IntScore) {
            IScoreMax = IntScore;
            FileStream FWScoreMax = new FileStream(Path, FileMode.Truncate);
            BinaryWriter WScoreMax = new BinaryWriter(FWScoreMax);
            WScoreMax.Write(IScoreMax);
            WScoreMax.Close();
            FWScoreMax.Close();
        }
        ScoreMax.text = IScoreMax.ToString();
        ScoreNow.text = IntScore.ToString();
    }
}
