using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexManager : MonoBehaviour {

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
    void Start () {

        current = this;

        gameBoard = new HexTileObject[boardWidth, boardHeight];
        legalHexes = new Dictionary<string, GameObject>();
        //hexQueue = new List<HexTileObject>();
        tilesInPlay = new List<HexTileObject>();

        CreateHexPrototypes();
        LoadSprites();
    }

    public void BeginGame()
    {
        PlaceHexOnBoard(HexPrototypes["Knight"], boardWidth / 2, boardHeight / 2);
        AddCardToQueue();
        ShowLegalHexes();
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
        if (GetHexAt(x - 1, y) == null && GetNeighbours(x - 1, y).Count == 1)
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


		tiles.Add( GetHexAt(x-1, y) ); // Left
		tiles.Add( GetHexAt(x+1, y) ); // Right
		tiles.Add( GetHexAt(x-1 + right_x_offset, y+1) ); // Top-Left
		tiles.Add( GetHexAt(x   + right_x_offset, y+1) ); // Top-Right
		tiles.Add( GetHexAt(x-1 + right_x_offset, y-1) ); // Top-Left
		tiles.Add( GetHexAt(x   + right_x_offset, y-1) ); // Top-Right

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


    void RemoveFromQueue(GameObject cardGO)
    {
        if (cardGO == null) // Usually with the bailout or other triggered card.
            return;

        //hexQueue.Remove(cardGO.GetComponent<HexTileObject>());
        cardGO.transform.SetParent(null); // I'm Batman
        /*
        //Destroy(cardGO);
        cardGO.GetComponent<PlaceHexButton>().isClickable = false;
        if (PlaceHexButton.selectedButton == cardGO.GetComponent<PlaceHexButton>())
        {
            PlaceHexButton.selectedButton = null;
        }
        */
    }

    void AddCardToQueue(HexTileData hexData)
    {
        HexTileObject hexObj = Instantiate(hexPrefab);
        hexObj.tileData = hexData;
        hexObj.owner = GameManager.current.getCurrentPlayer();
        hexObj.status = TileStatus.inQueue;
        //hexObj.GetComponentInChildren<TextMesh>(true).text = hexPrototypes_deck[cardName].cardName;
        //hexObj.SetActive(true);
        hexObj.GetComponent<SpriteRenderer>().color = Player.colors[hexObj.owner.index];
        hexObj.GetComponent<SpriteRenderer>().sprite = hexSprites[hexObj.tileData.name];
        hexObj.transform.SetParent(hexQueue.transform);
        hexObj.transform.localPosition = new Vector3(.5f, 0, 0);

        //cardQueue.Add(cardGO.GetComponent<HexCard>());
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
        if (queuedHex.status != TileStatus.OnBoard)
            return;
        //HexTileObject hexTile = queuedHex;
        queuedHex.status = TileStatus.Placed;
        List<HexTileObject> ns = GetNeighbours(queuedHex.x, queuedHex.y);

        Debug.Log(queuedHex + " has " + ns.Count + " neighbours.");

        queuedHex.neighbours = ns;

        foreach (HexTileObject n in ns)
        {
            n.neighbours.Add(queuedHex);
        }

        CalcLegalHexes(queuedHex);

        GameManager.current.NextPlayer();
        AddCardToQueue();
    }

    // Place a card that has already been instantiated for us.
    HexTileObject PlaceHexOnBoard(HexTileData card, int x, int y)
    {
      //  if (GameManager.current.IsGameOver())
         //   return null;

        HexTileObject hexTile = Instantiate(hexPrefab);
        hexTile.transform.position = CoordToWorld(x, y);
        //hexTile.status = TileStatus.Placed;
        //SetLayerRecursively(cardGO, 0);

        gameBoard[x, y] = hexTile;
        hexTile.x = x;
        hexTile.y = y;
        //tilesInPlay.Add(hexTile);

        //Destroy(cardGO.transform.Find("HexCardBackground(Clone)/TileBackground-Edge").gameObject);

        hexTile.status = TileStatus.Placed;
        List<HexTileObject> ns = GetNeighbours(hexTile.x, hexTile.y);

        hexTile.neighbours = ns;
        foreach (HexTileObject n in ns)
        {
            n.neighbours.Add(hexTile);
        }

        CalcLegalHexes(hexTile);

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
    

        void CreateHexPrototypes()
        {
            HexPrototypes = new Dictionary<string, HexTileData>();
            

        HexPrototypes.Add("Knight", new HexTileData("Knight", new int[6] { 1, 2, 3, 4, 5 ,6 }));
        //HexPrototypes.Add("King", new HexTileData("King", new int[6] { 1, 2, 3, 4, 5, 6 }));

    }
    }
