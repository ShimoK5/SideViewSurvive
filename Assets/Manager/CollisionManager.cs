using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

	public enum HIT_DIRECTION
	{
		NON_HIT,
		HIT_TOP,
		HIT_UNDER,
		HIT_RIGHT,
		HIT_LEFT,
		HIT_NANAME,
		HIT_UMORE,
		HIT_MAX
	};

	public struct TWO_OBJECT_DIRECTION
	{
		public HIT_DIRECTION ObjDirection1;
		public HIT_DIRECTION ObjDirection2;
	}
// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
		switch (GameStateManager.instance.GameState)
		{
			case GAME_STATE.StartPlayerMotion:
			case GAME_STATE.Game:
			case GAME_STATE.DeadPlayer:
			case GAME_STATE.DeadPlayerStop:
			case GAME_STATE.EndPlayerMotion:
				FixedGame();
				break;
			//default:
			//	FixedGame();
			//	break;
		}
	}

	void FixedGame()
    {
		//プレイヤー
		MoveObjHitBlock(Player.instance.GetM_Player());

		//エネミー
		Enemy[] Enemys = GameObject.FindObjectsOfType<Enemy>();
		for (int i = 0; i < Enemys.Length; i++)
		{
			MoveObjHitBlock(Enemys[i].GetM_Enemy());
		}

		//身代わり
		Sacrifice[] sacrifices = GameObject.FindObjectsOfType<Sacrifice>();
		for (int i = 0; i < sacrifices.Length; i++)
		{
			MoveObjHitBlock(sacrifices[i]);
		}
	}

	void MoveObjHitBlock(PawnIF pawn)
	{
		Block[] Blocks = GameObject.FindObjectsOfType<Block>();

		bool ObjHitUnder = false;
		bool MenHit = false;

		//食い込み判定
		bool UmoreFlag = false;

		int Under = -1;
		int Top = -1;
		int Left = -1;
		int Right = -1;

		
		//エネミーかどうか
		bool isEnemy = (bool)(pawn.GetType().IsSubclassOf(typeof(EnemyIF)));

		//斜め以外を先に見る   並んだブロックに当たるのを回避するため 
		for (int i = 0; i < Blocks.Length; i++)//全ブロック見るよ
		{
			//大人壁とエネミーのあたり判定をなくす
			if (isEnemy && Blocks[i].GetComponent<OtonaBlock>())
			{
				continue;
			}

			//横長ブロックと上下だけ取る
			if (Blocks[i])//i番目がnullではない (=使っているなら)
			{

				

				//プレイヤーのあたりかたで分岐
				switch (CollisionBB(pawn.tf.transform.position, pawn.OldPos, Blocks[i].transform.position, Blocks[i].OldPos, pawn.Size, Blocks[i].Size, false).ObjDirection1)
				{
					case HIT_DIRECTION.HIT_UMORE:
						MenHit = true;
						UmoreFlag = true;
						break;

					case HIT_DIRECTION.HIT_UNDER:
						if (Blocks[i].GetComponent<TopOnlyBlock2>() && InputManager_FU.instanse.GetKey(Key.Down))
						{
							Debug.Log("地形抜け");
							break;
						}
						MenHit = true;
						//pObj.HitUnder(CopyBlocks[i]);
						ObjHitUnder = true;
						if (Under == -1)
						{
							Under = i;
						}
						else
						{
							//今あるやつより近ければ
							if (Blocks[i].transform.position.y - Blocks[i].Size.y * 0.5f < Blocks[Under].transform.position.y - Blocks[Under].Size.y * 0.5f)
							{
								Under = i;
							}
						}
						break;

					case HIT_DIRECTION.HIT_TOP:
						//上だけブロックならやらない
						if(Blocks[i].GetComponent<TopOnlyBlock2>())
                        {
							break;
                        }


						MenHit = true;
						//pObj.HitTop(CopyBlocks[i]);
						if (Top == -1)
						{
							Top = i;
						}
						else
						{
							//今あるやつより近ければ
							if (Blocks[i].transform.position.y + Blocks[i].Size.y * 0.5f > Blocks[Top].transform.position.y + Blocks[Top].Size.y * 0.5f)
							{
								Top = i;
							}
						}
						break;

					case HIT_DIRECTION.HIT_RIGHT:
						//上だけブロックならやらない
						if (Blocks[i].GetComponent<TopOnlyBlock2>())
						{
							break;
						}


						MenHit = true;
						//pObj.HitRight(CopyBlocks[i]);
						if (Right == -1)
						{
							Right = i;
						}
						else
						{
							//今あるやつより近ければ
							if (Blocks[i].transform.position.x - Blocks[i].Size.x * 0.5f < Blocks[Right].transform.position.x - Blocks[Right].Size.x * 0.5f)
							{
								Right = i;
							}
						}
						break;

					case HIT_DIRECTION.HIT_LEFT:
						//上だけブロックならやらない
						if (Blocks[i].GetComponent<TopOnlyBlock2>())
						{
							break;
						}


						MenHit = true;
						//pObj.HitLeft(CopyBlocks[i]);
						if (Left == -1)
						{
							Left = i;
						}
						else
						{
							//今あるやつより近ければ
							if (Blocks[i].transform.position.x + Blocks[i].Size.x * 0.5f > Blocks[Left].transform.position.x + Blocks[Left].Size.x * 0.5f)
							{
								Left = i;
							}
						}
						break;

					default:
						break;
				}
			}
		}
	

		//当たらなかったから斜めも見る
		if (!MenHit)
		{
			for (int i = 0; i < Blocks.Length; i++)//全ブロック見るよ
			{
				//大人壁とエネミーのあたり判定をなくす
				if (isEnemy && Blocks[i].GetComponent<OtonaBlock>())
				{
					continue;
				}

				if (Blocks[i])//i番目がnullではない (=使っているなら)
				{
					//プレイヤーのあたりかたで分岐
					switch (CollisionBB(pawn.tf.transform.position, pawn.OldPos, Blocks[i].transform.position, Blocks[i].OldPos, pawn.Size , Blocks[i].Size, true).ObjDirection1)
					{
						case HIT_DIRECTION.HIT_UNDER:
							if (Blocks[i].GetComponent<TopOnlyBlock2>() && InputManager_FU.instanse.GetKey(Key.Down))
							{
								break;
							}

							//pObj.HitUnder(CopyBlocks[i]);
							Under = i;
							ObjHitUnder = true;
							break;

						case HIT_DIRECTION.HIT_TOP:
							//上だけブロックならやらない
							if (Blocks[i].GetComponent<TopOnlyBlock2>())
							{
								break;
							}


							//pObj.HitTop(CopyBlocks[i]);
							Top = i;
							break;

						case HIT_DIRECTION.HIT_RIGHT:
							//上だけブロックならやらない
							if (Blocks[i].GetComponent<TopOnlyBlock2>())
							{
								break;
							}


							//pObj.HitRight(CopyBlocks[i]);
							Right = i;
							break;

						case HIT_DIRECTION.HIT_LEFT:
							//上だけブロックならやらない
							if (Blocks[i].GetComponent<TopOnlyBlock2>())
							{
								break;
							}


							//pObj.HitLeft(CopyBlocks[i]);
							Left = i;
							break;

						default:
							break;
					}
				}
			}
		}

		if (Under != -1)
        {
			pawn.HitUnder(Blocks[Under]);
			//Debug.Log("下ヒット");
		}
			

		if (Top != -1)
        {
			pawn.HitTop(Blocks[Top]);
			//Debug.Log("上ヒット");
		}
			

		if (Left != -1)
        {
			pawn.HitLeft(Blocks[Left]);
			//Debug.Log("左ヒット");
		}
			

		if (Right != -1)
        {
			pawn.HitRight(Blocks[Right]);
			//Debug.Log("右ヒット");
		}
			

		//どこでも下に当たっていなければ
		if (!ObjHitUnder)
		{
			//pObj.isGround = false;
			pawn.NonHitUnder();
		}

		//埋もれていれば
		if(UmoreFlag)
        {
			//埋もれている時ようの処理
			pawn.Umore();
        }

	}



	TWO_OBJECT_DIRECTION CollisionBB(Vector3 pos1, Vector3 old_pos1, Vector3 pos2, Vector3 old_pos2, Vector3 size1, Vector3 size2, bool naname_check)
	{
		Vector2 Min1, Max1;//オブジェクト１の左上、右上の頂点座標を格納
		Vector2 Min2, Max2;//オブジェクト２の左上、右上の頂点座標を格納

		TWO_OBJECT_DIRECTION ReturnTwoDirection;//各オブジェクトのどの面に当たったのかをこの変数に入れてreturnする
		ReturnTwoDirection.ObjDirection1 = HIT_DIRECTION.NON_HIT;
		ReturnTwoDirection.ObjDirection2 = HIT_DIRECTION.NON_HIT;

		Min1.x = pos1.x - size1.x / 2;
		Min1.y = pos1.y - size1.y / 2;
		Max1.x = pos1.x + size1.x / 2;
		Max1.y = pos1.y + size1.y / 2;

		Min2.x = pos2.x - size2.x / 2;
		Min2.y = pos2.y - size2.y / 2;
		Max2.x = pos2.x + size2.x / 2;
		Max2.y = pos2.y + size2.y / 2;

		if (Min1.x >= Max2.x || Max1.x <= Min2.x || //X軸の判定
			Min1.y >= Max2.y || Max1.y <= Min2.y //Y軸の判定
			)
		{
			//全ての条件がFalseなら当たっていない
			ReturnTwoDirection.ObjDirection1 = HIT_DIRECTION.NON_HIT;
			ReturnTwoDirection.ObjDirection2 = HIT_DIRECTION.NON_HIT;
			return ReturnTwoDirection;
		}

		// ↑で return を通ってたら、この下のプログラムは読まない！！！(returnでこの関数から抜けているので)

		//   以下、どの面に当たっているか、を探索する
		Vector2 ObjVertualVel1 = (pos1 - old_pos1) - (pos2 - old_pos2);//obj２のVelの逆を１に足すことで、2から見た1の移動Velを作っている
#if true
	//数学的に正しいけどfloatの誤差出る
	Vector2 VertualOldMin1 = Min1 - ObjVertualVel1;//2を基準にした1フレ前の仮想obj1の頂点座標
	Vector2 VertualOldMax1 = Max1 - ObjVertualVel1;//                〃
#else
		Vector2 VertualOldMin1 = new Vector2(old_pos1.x - size1.x / 2, old_pos1.y - size1.y / 2);//2を基準にした1フレ前の仮想obj1の頂点座標
		Vector2 VertualOldMax1 = new Vector2(old_pos1.x + size1.x / 2, old_pos1.y + size1.y / 2);//                〃
#endif // 0

		//元々埋もれている （当たらない	）
		if(VertualOldMax1.x - 0.01f > Min2.x && VertualOldMin1.x + 0.01f < Max2.x &&
			VertualOldMax1.y - 0.01f > Min2.y && VertualOldMin1.y + 0.01f < Max2.y)
        {
			ReturnTwoDirection.ObjDirection1 = HIT_DIRECTION.HIT_UMORE;
			ReturnTwoDirection.ObjDirection2 = HIT_DIRECTION.HIT_UMORE;
			Debug.Log("埋もれ Pos = " + pos1 + "OldPos = " + old_pos1);
		}
		//元々上辺or下辺に衝突する位置にいた
		else if (VertualOldMin1.x < Max2.x && VertualOldMax1.x > Min2.x)
		{
			if (ObjVertualVel1.y >= 0.0f)//仮想的にoblj１が下向きに進んでいる
			{
				ReturnTwoDirection.ObjDirection1 = HIT_DIRECTION.HIT_TOP;
				ReturnTwoDirection.ObjDirection2 = HIT_DIRECTION.HIT_UNDER;

			}
			else//上向き
			{
				ReturnTwoDirection.ObjDirection1 = HIT_DIRECTION.HIT_UNDER;
				ReturnTwoDirection.ObjDirection2 = HIT_DIRECTION.HIT_TOP;

			}

		}

		//元々右辺or左辺に衝突する位置にいた
		else if (VertualOldMin1.y < Max2.y && VertualOldMax1.y > Min2.y)
		{
			if (ObjVertualVel1.x >= 0.0f)//仮想的にoblj１が右向きに進んでいる
			{
				ReturnTwoDirection.ObjDirection1 = HIT_DIRECTION.HIT_RIGHT;
				ReturnTwoDirection.ObjDirection2 = HIT_DIRECTION.HIT_LEFT;
			}
			else//左向き
			{
				ReturnTwoDirection.ObjDirection1 = HIT_DIRECTION.HIT_LEFT;
				ReturnTwoDirection.ObjDirection2 = HIT_DIRECTION.HIT_RIGHT;
			}
		}

		///////////程度に関わらず 仮想的に斜めに衝突している
		else
		{
			//Debug.Log("斜め");
			CollisionNaname(ref ReturnTwoDirection, ObjVertualVel1, VertualOldMax1, VertualOldMin1, Max2, Min2, naname_check);
		}

		return ReturnTwoDirection;
	}

	void CollisionNaname(ref TWO_OBJECT_DIRECTION direction, Vector2 obj_vertual_vel1, Vector2 max1, Vector2 min1, Vector2 max2, Vector2 min2, bool naname_check)
	{
		if (naname_check)
		{
			float VertualVelAngle = Mathf.Atan2(obj_vertual_vel1.y, obj_vertual_vel1.x);
			//頂点のアングルに使う
			Vector2 vertual_old_max1 = max1 - obj_vertual_vel1;
			Vector2 vertual_old_min1 = min1 - obj_vertual_vel1;

			//obj1が仮想的に下よりに進んでいる
			if (VertualVelAngle >= 0.0f)
			{
				//仮想的にobj1が右下に進んでいる
				if (VertualVelAngle <= Mathf.PI / 2)
				{
					float VertexAngle = Mathf.Atan2(min2.y - max1.y, min2.x - max1.x);//過去のobj1右下頂点から見た、obj2左上頂点の角度
					if (VertualVelAngle > VertexAngle)//仮想velが頂点へのvelより大きかった
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_RIGHT;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_LEFT;
					}
					else
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_TOP;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_UNDER;
					}
				}
				//仮想的にobj1が左下に進んでいる
				else
				{
					float VertexAngle = Mathf.Atan2(min2.y - max1.y, max2.x - min1.x);//過去のobj1左下頂点から見た、obj2右上頂点の角度
					if (VertualVelAngle > VertexAngle)//仮想velが頂点へのvelより大きかった
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_TOP;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_UNDER;
					}
					else
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_LEFT;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_RIGHT;
					}
				}
			}
			//obj1が仮想的に上よりに進んでいる
			else
			{
				//仮想的にobj1が右上に進んでいる
				if (VertualVelAngle >= -Mathf.PI / 2)
				{
					float VertexAngle = Mathf.Atan2(max2.y - min1.y, min2.x - max1.x);//過去のobj1右上頂点から見た、obj2左下頂点の角度
					if (VertualVelAngle > VertexAngle)//仮想velが頂点へのvelより大きかった
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_UNDER;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_TOP;
					}
					else
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_RIGHT;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_LEFT;
					}
				}
				//仮想的にobj1が左上に進んでいる
				else
				{
					float VertexAngle = Mathf.Atan2(max2.y - min1.y, max2.x - min1.x);//過去のobj1左上頂点から見た、obj2右下頂点の角度
					if (VertualVelAngle > VertexAngle)//仮想velが頂点へのvelより大きかった
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_LEFT;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_RIGHT;
					}
					else
					{
						//Debug.Log(VertexAngle);
						direction.ObjDirection1 = HIT_DIRECTION.HIT_UNDER;
						direction.ObjDirection2 = HIT_DIRECTION.HIT_TOP;
					}
				}
			}
		}

		else
		{
			direction.ObjDirection1 = HIT_DIRECTION.HIT_NANAME;
			direction.ObjDirection2 = HIT_DIRECTION.HIT_NANAME;
		}
	}
}
