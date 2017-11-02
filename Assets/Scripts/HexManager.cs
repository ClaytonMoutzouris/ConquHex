using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexManager : MonoBehaviour {

    public static Vector2[] startPos = new Vector2[4] { new Vector2(boardWidth / 2, boardHeight / 2), new Vector2(boardWidth / 2 +1, boardHeight / 2), new Vector2(boardWidth / 2, boardHeight / 2 +1), new Vector2(boardWidth / 2 + 1, boardHeight / 2 +1) };
    public static HexManager current;
    HexTileObject tileToPlace;
    public HexTileObject queuedHex;
    public GameObject tileBackPrefab;
    public HexTileObject hexPrefab;
    public GameObject legalHexPrefab;
    Dictionary<string, GameObject> legalHexes;
    Dictionary<string, HexTileData> hexPrototypes;
    Dictionary<string, Sprite> hexSprites;

    HexTileObject[,] gameBoard;
    List<HexTileObject> tilesInPlay;

    const int boardWidth = 50;
    const int boardHeight = 50;

    //List<HexTileObject> hexQueue;
    public GameObject hexQueue;
    // public GameObject tileQueueInstance;

    public Dictionary<string, HexTileData> HexPrototypes
    {
        get
        {
            return hexPrototypes;
        }

        set
        {
            hexPrototypes = value;
        }
    }
    // Use this for initialization

    public void Start()
    {
        current = this;
    }

    public void Init () {



        gameBoard = new HexTileObject[boardWidth, boardHeight];
        legalHexes = new Dictionary<string, GameObject>();
        //hexQueue = new List<HexTileObject>();
        tilesInPlay = new List<HexTileObject>();

        CreateHexPrototypes();
        LoadSprites();
    }

    public void PlayAgain()
    {

    }

    public void BeginGame()
    {
        SetUpBoard();
        //PlaceHexOnBoard(, boardWidth / 2, boardHeight / 2);
        AddCardToQueue();
        ShowLegalHexes();
    }

    public void SetUpBoard()
    {
        foreach(Player p in GameManager.current.Players)
        {
            PlaceStartingHex((int)startPos[p.index].x, (int)startPos[p.index].y, p);

        }

    }

    void LoadSprites()
    {
        hexSprites = new Dictionary<string, Sprite>();
        //hexSprites = Resources.LoadAll<Sprite>("");
        foreach(KeyValuePair<string, HexTileData> hex in HexPrototypes)
        {
            hexSprites.Add(hex.Key, Resources.Load<Sprite>("TileSprite_" + hex.Key));
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ShowLegalHexes()
    {
        foreach (GameObject go in legalHexes.Values)
        {
            HexLegal hex = go.GetComponent<HexLegal>();

            
                go.SetActive(true);

        }
    }

    void HideLegalHexes()
    {
        foreach (GameObject go in legalHexes.Values)
        {
            go.SetActive(false);
        }
    }

    void CalcLegalHexes(HexTileObject placedTile)
    {
        int x = placedTile.x;
        int y = placedTile.y;

        // Remove the legal tile at the x,y
        // This will be false for the HQ turn.
        if (legalHexes.ContainsKey(x + "_" + y))
        {
            Destroy(legalHexes[x + "_" + y]);
            legalHexes.Remove(x + "_" + y);
        }



        // On even numbered rows, tiles to the right have the same X.
        // On even numbered rows, tiles to the left have  X-1.
        // Opposite for odds.
        int right_x_offset = (y % 2);

        // Create any new HexLegals needed.
        if (GetHexAt(x - 1, y) == null  && GetNeighbours(x - 1, y).Count == 1)
        {
            GameObject go = (GameObject)Instantiate(legalHexPrefab, CoordToWorld(x - 1, y), Quaternion.identity);
            go.GetComponent<HexLegal>().x = x - 1;
            go.GetComponent<HexLegal>().y = y;

            legalHexes.Add(go.GetComponent<HexLegal>().x + "_" + go.GetComponent<HexLegal>().y, go);
        }
        if (GetHexAt(x + 1, y) == null && GetNeighbours(x + 1, y).Count == 1)
        {
            GameObject go = (GameObject)Instantiate(legalHexPrefab, CoordToWorld(x + 1, y), Quaternion.identity);
            go.GetComponent<HexLegal>().x = x + 1;
            go.GetComponent<HexLegal>().y = y;

            legalHexes.Add(go.GetComponent<HexLegal>().x + "_" + go.GetComponent<HexLegal>().y, go);
        }
        if (GetHexAt(x - 1 + right_x_offset, y + 1) == null && GetNeighbours(x - 1 + right_x_offset, y + 1).Count == 1)
        {
            GameObject go = (GameObject)Instantiate(legalHexPrefab, CoordToWorld(x - 1 + right_x_offset, y + 1), Quaternion.identity);
            go.GetComponent<HexLegal>().x = x - 1 + right_x_offset;
            go.GetComponent<HexLegal>().y = y + 1;

            legalHexes.Add(go.GetComponent<HexLegal>().x + "_" + go.GetComponent<HexLegal>().y, go);
        }
        if (GetHexAt(x + right_x_offset, y + 1) == null && GetNeighbours(x + right_x_offset, y + 1).Count == 1)
        {
            GameObject go = (GameObject)Instantiate(legalHexPrefab, CoordToWorld(x + right_x_offset, y + 1), Quaternion.identity);
            go.GetComponent<HexLegal>().x = x + right_x_offset;
            go.GetComponent<HexLegal>().y = y + 1;

            legalHexes.Add(go.GetComponent<HexLegal>().x + "_" + go.GetComponent<HexLegal>().y, go);
        }
        if (GetHexAt(x - 1 + right_x_offset, y - 1) == null && GetNeighbours(x - 1 + right_x_offset, y - 1).Count == 1)
        {
            GameObject go = (GameObject)Instantiate(legalHexPrefab, CoordToWorld(x - 1 + right_x_offset, y - 1), Quaternion.identity);
            go.GetComponent<HexLegal>().x = x - 1 + right_x_offset;
            go.GetComponent<HexLegal>().y = y - 1;

            legalHexes.Add(go.GetComponent<HexLegal>().x + "_" + go.GetComponent<HexLegal>().y, go);
        }
        if (GetHexAt(x + right_x_offset, y - 1) == null && GetNeighbours(x + right_x_offset, y - 1).Count == 1)
        {
            GameObject go = (GameObject)Instantiate(legalHexPrefab, CoordToWorld(x + right_x_offset, y - 1), Quaternion.identity);
            go.GetComponent<HexLegal>().x = x + right_x_offset;
            go.GetComponent<HexLegal>().y = y - 1;

            legalHexes.Add(go.GetComponent<HexLegal>().x + "_" + go.GetComponent<HexLegal>().y, go);
        }

        CalcLegalNeighbour();
        //HideLegalHexes();
    }

    void CalcLegalNeighbour()
    {
        foreach (GameObject hexGO in legalHexes.Values)
        {
            HexLegal hex = hexGO.GetComponent<HexLegal>();

            if (hex == null)
            {
                Debug.LogError(hexGO.name + " has no HexLegal component?");
            }

            hex.legalNeighbours = GetLegalNeighbours(hex.x, hex.y);
        }
    }

    List<HexTileObject> GetNeighbours(int x, int y) {
		List<HexTileObject> tiles = new List<HexTileObject>();

		// On even numbered rows, tiles to the right have the same X.
		// On even numbered rows, tiles to the left have  X-1.
		// Opposite for odds.
		int right_x_offset = (y % 2); 


		tiles.Add( GetHexAt(x-1, y) );                   // Left           -- 0
        
		tiles.Add( GetHexAt(x-1 + right_x_offset, y+1) ); // Top-Left      -- 1
		tiles.Add( GetHexAt(x   + right_x_offset, y+1) ); // Top-Right     -- 2
        tiles.Add( GetHexAt(x + 1, y) );                // Right           -- 3
		tiles.Add( GetHexAt(x   + right_x_offset, y-1) ); // Bottom-Right  -- 4
        tiles.Add(GetHexAt(x - 1 + right_x_offset, y - 1)); // Bottom-Left -- 5

        
        int i=0;
		while (i < tiles.Count) {
			if(tiles[i] == null) {
				tiles.RemoveAt(i);
			}
			else {
				i++;
			}
		}
        

		return tiles; // Can contain nulls!
	}

    public void CalculateNeighbours(HexTileObject hex)
    {

        int right_x_offset = (hex.y % 2);
        hex.neighbourPositions = new Dictionary<Edge, HexTileObject>();
        hex.neighbourPositions.Add(Edge.Left, GetHexAt(hex.x - 1, hex.y)); // Left           -- 0

        hex.neighbourPositions.Add(Edge.TopLeft, GetHexAt(hex.x - 1 + right_x_offset, hex.y + 1)); // Top-Left      -- 1
        hex.neighbourPositions.Add(Edge.TopRight, GetHexAt(hex.x + right_x_offset, hex.y + 1)); // Top-Right     -- 2
        hex.neighbourPositions.Add(Edge.Right, GetHexAt(hex.x + 1, hex.y));                // Right           -- 3
        hex.neighbourPositions.Add(Edge.BottomRight, GetHexAt(hex.x + right_x_offset, hex.y - 1)); // Bottom-Right  -- 4
        hex.neighbourPositions.Add(Edge.BottomLeft, GetHexAt(hex.x - 1 + right_x_offset, hex.y - 1)); // Bottom-Left -- 5
    }


	// Returns a list of HexLegal spots adjacent to this.
	List<HexLegal> GetLegalNeighbours(int x, int y) {
		List<HexLegal> legals = new List<HexLegal>();

		// On even numbered rows, tiles to the right have the same X.
		// On even numbered rows, tiles to the left have  X-1.
		// Opposite for odds.
		int right_x_offset = (y % 2); 


		try{ legals.Add( legalHexes[(x-1) +"_"+ y].GetComponent<HexLegal>() ); } catch {} // Left
		try{ legals.Add( legalHexes[(x+1)+"_"+ y].GetComponent<HexLegal>() ); } catch {} // Right
		try{ legals.Add( legalHexes[(x-1 + right_x_offset)+"_"+ (y+1)].GetComponent<HexLegal>() ); } catch {}// Top-Left
		try{ legals.Add( legalHexes[(x   + right_x_offset)+"_"+ (y+1)].GetComponent<HexLegal>() ); } catch {}// Top-Right
		try{ legals.Add( legalHexes[((x-1) + right_x_offset)+"_"+ (y-1)].GetComponent<HexLegal>() ); } catch {}// Top-Left
		try{ legals.Add( legalHexes[(x   + right_x_offset)+"_"+ (y-1)].GetComponent<HexLegal>() ); } catch {}// Top-Right

        
		int i=0;
		while (i < legals.Count) {
			if(legals[i] == null) {
				legals.RemoveAt(i);
			}
			else {
				i++;
			}
		}
        
		return legals; // Can contain nulls!
	}

	HexTileObject GetHexAt(int x, int y) {
		if( x < 0 || x >= boardWidth || y < 0 || y >= boardHeight)
			return null;

		return gameBoard[x,y]; // Might still be null if no tile there yet!
	}


    void AddCardToQueue(HexTileData hexData)
    {
        HexTileObject hexObj = Instantiate(hexPrefab);
        hexObj.tileData = hexData.Clone();
        hexObj.owner = GameManager.current.getCurrentPlayer();
        hexObj.status = TileStatus.inQueue;
        //hexObj.GetComponentInChildren<TextMesh>(true).text = hexPrototypes_deck[cardName].cardName;
        //hexObj.SetActive(true);
        hexObj.GetComponent<SpriteRenderer>().color = hexObj.owner.pColor;
        hexObj.GetComponent<SpriteRenderer>().sprite = hexSprites[hexObj.tileData.name];
        hexObj.transform.SetParent(hexQueue.transform);
        hexObj.transform.localPosition = new Vector3(0, 0, 0);

        //cardQueue.Add(cardGO.GetComponent<HexCard>());
        //queuedHex.c
        queuedHex = hexObj;
        SetLayerRecursively(hexObj.gameObject, hexQueue.layer);
        

        //hexObj.GetComponent<SpriteRenderer>().sortingOrder

        // RefreshCardQueue();
    }


    void AddCardToQueue()
    {

        HexTileData hexData = GameManager.current.getCurrentPlayer().Deck.GetNextHex();
        if(hexData != null)
        AddCardToQueue(hexData);
    }



    public void PlaceQueuedHexOnBoard(int x, int y)
    {

        if (GameManager.current.IsGameOver)
            return;

        if(queuedHex.status == TileStatus.OnBoard)
        {
            gameBoard[queuedHex.x, queuedHex.y] = null;
        }
        queuedHex.status = TileStatus.OnBoard;
        //HexTileObject hexTile = queuedHex;

        queuedHex.transform.SetParent(this.transform);
        SetLayerRecursively(queuedHex.gameObject, gameObject.layer);
        queuedHex.transform.position = CoordToWorld(x, y);
        //SetLayerRecursively(cardGO, 0);
        //hexTile.status = TileStatus.Placed;
        gameBoard[x, y] = queuedHex;
        queuedHex.x = x;
        queuedHex.y = y;
        //tilesInPlay.Add(hexTile);



    }

    public void ConfirmHex()
    {
        if (GameManager.current.IsGameOver)
        {
            return;
        }
        if (queuedHex.status != TileStatus.OnBoard)
            return;
        //HexTileObject hexTile = queuedHex;
        queuedHex.status = TileStatus.Placed;
        

        List<HexTileObject> ns = GetNeighbours(queuedHex.x, queuedHex.y);

        
        //Debug.Log(queuedHex + " has " + ns.Count + " neighbours.");

        queuedHex.neighbours = ns;
        //lets add this hex to its neighbours neighbours
        foreach (HexTileObject n in ns)
        {
            if(n != null)
            n.neighbours.Add(queuedHex);
        }
        //add the new legal positions created by placing this hex
        CalcLegalHexes(queuedHex);
        //calculate the scoring for placing this hex
        CalculateScore();
        AudioSource.PlayClipAtPoint(AudioManager.current.soundFX[3], Camera.main.transform.position, 0.5f);
        //Check if the game is over
        if (GameManager.current.checkGameOver())
        {
            GameManager.current.IsGameOver = true;
            UIManager.current.GameOver();
            return;
        }
        //if the game isnt over, get next player
        GameManager.current.NextPlayer();
        AddCardToQueue();
    }

    public void CalculateScore()
    {
        int outcome = 0;
        CalculateNeighbours(queuedHex);

        foreach(KeyValuePair<Edge, HexTileObject> n in queuedHex.neighbourPositions)
        {
            //Debug.Log(n.Key.ToString() + " is " + queuedHex.tileData.edges[n.Key].ToString());
            if (n.Value == null)
                continue;

            if(queuedHex.owner != n.Value.owner)
            {
                outcome = queuedHex.tileData.Battle(n.Value.tileData, n.Key);
                if (outcome > 0)
                {
                    queuedHex.owner.Score += 2;
                }
                else if (outcome < 0)
                {
                    n.Value.owner.Score += 2;
                    UIManager.current.UpdateScore(n.Value.owner);
                }
            } else
            {
                if(queuedHex.tileData.Assist(n.Value.tileData, n.Key))
                {
                    queuedHex.owner.Score += 1;
                }

            }
        }

        UIManager.current.UpdateScore(queuedHex.owner);
    }

    public void CalculateScore(HexTileObject hex)
    {
        int score = 0;
        CalculateNeighbours(hex);

        foreach (KeyValuePair<Edge, HexTileObject> n in hex.neighbourPositions)
        {
            //Debug.Log(n.Key.ToString() + " is " + queuedHex.tileData.edges[n.Key].ToString());
            if (n.Value == null)
                continue;

            if (hex.owner != n.Value.owner)
            {
                score = hex.tileData.Battle(n.Value.tileData, n.Key);
                if (score >= 0)
                {
                    hex.owner.Score += score;
                }
                else
                {
                    n.Value.owner.Score += Mathf.Abs(score);
                    UIManager.current.UpdateScore(n.Value.owner);
                }
            }
            else
            {
                if (queuedHex.tileData.Assist(n.Value.tileData, n.Key))
                {
                    hex.owner.Score += 1;
                }

            }
        }

        UIManager.current.UpdateScore(hex.owner);
    }


    // Place a card that has already been instantiated for us.
    HexTileObject PlaceStartingHex(int x, int y, Player o)
    {
      //  if (GameManager.current.IsGameOver())
         //   return null;

        HexTileObject hexTile = Instantiate(hexPrefab);
        hexTile.tileData = o.Deck.GetNextHex();
        hexTile.transform.position = CoordToWorld(x, y);
        hexTile.owner = o;
        //hexTile.status = TileStatus.Placed;
        //SetLayerRecursively(cardGO, 0);
        hexTile.GetComponent<SpriteRenderer>().color = hexTile.owner.pColor;
        hexTile.GetComponent<SpriteRenderer>().sprite = hexSprites[hexTile.tileData.name];

        gameBoard[x, y] = hexTile;
        hexTile.x = x;
        hexTile.y = y;
        //tilesInPlay.Add(hexTile);

        hexTile.RotateHex(Random.Range(0, 6));

        //Destroy(cardGO.transform.Find("HexCardBackground(Clone)/TileBackground-Edge").gameObject);

        hexTile.status = TileStatus.Placed;
        List<HexTileObject> ns = GetNeighbours(hexTile.x, hexTile.y);

        hexTile.neighbours = ns;
        foreach (HexTileObject n in ns)
        {
            if(n!= null)
            n.neighbours.Add(hexTile);
            
        }

        CalcLegalHexes(hexTile);
        CalculateScore(hexTile);
        //Destroy( cardGO.GetComponent<PlaceHexButton>() );

        return hexTile;

    }


    public void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }


    const float hex_x_offset = 0.869f;
    const float hex_y_offset = 0.758f;



    public Vector2 CoordToWorld(int x, int y)
    {
        if (y % 2 == 0)
        {
            // Even row.
            return new Vector2(x * hex_x_offset, y * hex_y_offset);
        }

        // Odd row
        return new Vector2((x + 0.5f) * hex_x_offset, y * hex_y_offset);
    }
    
    //Create prototypes of all the different types of Hexes. When we make a hex, it uses this as its "card" data.
        void CreateHexPrototypes()
        {
            HexPrototypes = new Dictionary<string, HexTileData>();

        HexPrototypes.Add("King", new HexTileData("King", 1,
            new HexEdges(
                CombatSymbol.Magic, //Left
                CombatSymbol.Magic, //TopLeft
                CombatSymbol.Sword, //TopRight
                CombatSymbol.Shield, //Right
                CombatSymbol.Shield, //BottomRight
                CombatSymbol.Sword))); //BottomLeft

        HexPrototypes.Add("Wizard", new HexTileData("Wizard", 1,
            new HexEdges(
                CombatSymbol.Shield, //Left
                CombatSymbol.Magic, //TopLeft
                CombatSymbol.Sword, //TopRight
                CombatSymbol.Sword, //Right
                CombatSymbol.Magic, //BottomRight
                CombatSymbol.Shield))); //BottomLeft

        HexPrototypes.Add("Queen", new HexTileData("Queen", 1,
            new HexEdges(
                CombatSymbol.Sword, //Left
                CombatSymbol.Sword, //TopLeft
                CombatSymbol.Shield, //TopRight
                CombatSymbol.Magic, //Right
                CombatSymbol.Magic, //BottomRight
                CombatSymbol.Shield))); //BottomLeft

        HexPrototypes.Add("Knight", new HexTileData("Knight", 2,
           new HexEdges(
               CombatSymbol.Sword, //Left
               CombatSymbol.Magic, //TopLeft
               CombatSymbol.Sword, //TopRight
               CombatSymbol.Sword, //Right
               CombatSymbol.Sword, //BottomRight
               CombatSymbol.Sword))); //BottomLeft

        HexPrototypes.Add("Bishop", new HexTileData("Bishop", 2,
            new HexEdges(
                CombatSymbol.Magic, //Left
                CombatSymbol.Magic, //TopLeft
                CombatSymbol.Shield, //TopRight
                CombatSymbol.Magic, //Right
                CombatSymbol.Magic, //BottomRight
                CombatSymbol.Magic))); //BottomLeft

        HexPrototypes.Add("Rook", new HexTileData("Rook", 2,
            new HexEdges(
                CombatSymbol.Shield, //Left
                CombatSymbol.Sword, //TopLeft
                CombatSymbol.Shield, //TopRight
                CombatSymbol.Shield, //Right
                CombatSymbol.Shield, //BottomRight
                CombatSymbol.Shield))); //BottomLeft


        HexPrototypes.Add("Soldier", new HexTileData("Soldier", 3,
            new HexEdges(
                CombatSymbol.Sword, //Left
                CombatSymbol.Sword, //TopLeft
                CombatSymbol.Sword, //TopRight
                CombatSymbol.Shield, //Right
                CombatSymbol.Shield, //BottomRight
                CombatSymbol.Shield))); //BottomLeft

        HexPrototypes.Add("Mage", new HexTileData("Mage", 3,
           new HexEdges(
               CombatSymbol.Magic, //Left
               CombatSymbol.Magic, //TopLeft
               CombatSymbol.Magic, //TopRight
               CombatSymbol.Sword, //Right
               CombatSymbol.Sword, //BottomRight
               CombatSymbol.Sword))); //BottomLeft

        HexPrototypes.Add("Priest", new HexTileData("Priest", 3,
           new HexEdges(
               CombatSymbol.Shield, //Left
               CombatSymbol.Shield, //TopLeft
               CombatSymbol.Shield, //TopRight
               CombatSymbol.Magic, //Right
               CombatSymbol.Magic, //BottomRight
               CombatSymbol.Magic))); //BottomLeft

        //HexPrototypes.Add("King", new HexTileData("King", new int[6] { 1, 2, 3, 4, 5, 6 }));

    }
    }
