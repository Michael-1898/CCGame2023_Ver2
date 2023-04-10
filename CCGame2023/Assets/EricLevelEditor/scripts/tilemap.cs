using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class tilemap : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap tilemap1;
    public static List<Tile> allTiles = new List<Tile>();
    public static List<string> allTileCharacters = new List<string>();
    public static List<GameObject> allTileGameObjects = new List<GameObject>();
    public static List<Vector3> allTileSizes = new List<Vector3>();
    public static List<int> allTileLimits = new List<int>();

    public List<int> allTileCurrentNumbers = new List<int>();
    public List<Button> allTileButtons = new List<Button>();
    [SerializeField] private Text levelNameText;
    [SerializeField] private Text creatorNameText;
    [SerializeField] private Dropdown winCondition;
    [SerializeField] private string levelInformation;
    [SerializeField] private Tile xTile;
    [SerializeField] private Tile deleteTile;
    [SerializeField] private Tile rectangleToolTile;
    public GameObject tilePreview;
    SpriteRenderer tilePreviewSR;
    Tile currentTile;
    int currentTileIndex;
    Vector3Int currentDeletionLocation;
    Vector3 currentTileSize;
    bool delete = false;
    bool rectangleTool = false;
    bool info = false;
    Vector3 rectangleToolFirstClickLocation = new Vector3(-100f, -100f, 0f);
    

    public int columns;
    public int rows;
    

    
    
    void Start()
    {
        tilePreviewSR = tilePreview.GetComponent<SpriteRenderer>();
        Cursor.lockState = CursorLockMode.Confined;

        for (int i = 0; i < allTileLimits.Count; i++)
        {
            allTileCurrentNumbers.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTile != null && (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < (Camera.main.orthographicSize * (16f/9f))-6.2f+Camera.main.transform.position.x))
        {
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tilePreview.transform.position = new Vector3(Mathf.Floor(mousePos.x)+0.5f, Mathf.Floor(mousePos.y)+0.5f, 0f);
            
            if (currentTileSize != new Vector3(1f, 1f, 0f) && delete == false && rectangleTool == false)
            {
                tilePreviewSR.sprite = currentTile.sprite;
                
                bool noXTiles = true;
                for (int w = 0; w < (currentTileSize.x * currentTileSize.y); w++)
                {
                    if (tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt((tilePreview.transform.position.x - 0.5f) + (w % currentTileSize.x)), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f) + Mathf.FloorToInt(w / currentTileSize.x), 0)) == xTile)
                    {
                        noXTiles = false;
                    }
                }
                if (noXTiles)
                {
                   
                    if(allTileLimits[currentTileIndex] > allTileCurrentNumbers[currentTileIndex])
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject.Find("Warning Text").GetComponent<Text>().text = "";
                            if (tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0)) != xTile)
                            {
                                currentDeletionLocation = new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0);
                                deleteXTiles();
                                tilemap1.SetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0), currentTile);
                                allTileCurrentNumbers[currentTileIndex]++;
                            }
                            for (int m = 1; m < (currentTileSize.x * currentTileSize.y); m++)
                            {
                                if (tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt((tilePreview.transform.position.x - 0.5f) + (m % currentTileSize.x)), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f) + Mathf.FloorToInt(m / currentTileSize.x), 0)) != xTile)
                                {
                                    currentDeletionLocation = new Vector3Int(Mathf.FloorToInt((tilePreview.transform.position.x - 0.5f) + (m % currentTileSize.x)), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f) + Mathf.FloorToInt(m / currentTileSize.x), 0);
                                    deleteXTiles();
                                }
                                tilemap1.SetTile(new Vector3Int(Mathf.FloorToInt((tilePreview.transform.position.x - 0.5f) + (m % currentTileSize.x)), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f) + Mathf.FloorToInt(m / currentTileSize.x), 0), xTile);
                            }
                        }
                    }
                    else
                    {
                        GameObject.Find("Warning Text").GetComponent<Text>().text = "Maximum number of the object/tile";
                    }
                           
                   
                }
            }
            else if(delete == false && rectangleTool == false)
            {
                tilePreviewSR.sprite = currentTile.sprite;
                
                if(allTileLimits[currentTileIndex] > allTileCurrentNumbers[currentTileIndex])
                {
                    if(Input.GetMouseButton(0))
                    {
                        GameObject.Find("Warning Text").GetComponent<Text>().text = "";
                        if (tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0))!=xTile && tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0)) != currentTile)
                        {
                            currentDeletionLocation = new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0);
                            deleteXTiles();
                            tilemap1.SetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0), currentTile);
                            allTileCurrentNumbers[currentTileIndex]++;
                        }
                    }
                        
                }
                else GameObject.Find("Warning Text").GetComponent<Text>().text = "Maximum number of the object/tile";

                
            }
            else if(delete == true)
            {

                tilePreviewSR.sprite = GameObject.Find("delete").GetComponent<Image>().sprite;
                if (Input.GetMouseButton(0))
                {
                    if (tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0))!=xTile)
                    {
                        currentDeletionLocation = new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0);
                        deleteXTiles();

                        TileBase tempTile = tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0));
                        tilemap1.SetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0), null);
                        
                        for(int z = 0; z < allTileCharacters.Count; z++)
                        {
                            if(allTiles[z] == tempTile)
                            {
                                allTileCurrentNumbers[z]--;
                            }
                        }
                        
                    }
                }
            }
            else if(rectangleTool == true)
            {
                tilePreviewSR.sprite = GameObject.Find("rectangleTool").GetComponent<Image>().sprite;
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (tilemap1.GetTile(new Vector3Int(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0))!=xTile)
                    {
                        if(rectangleToolFirstClickLocation == new Vector3(-100f, -100f, 0f))
                        {
                            rectangleToolFirstClickLocation = new Vector3(Mathf.FloorToInt(tilePreview.transform.position.x - 0.5f), Mathf.FloorToInt(tilePreview.transform.position.y - 0.5f), 0);
                        }
                        else if(mousePos.x > rectangleToolFirstClickLocation.x + 1f && mousePos.y > rectangleToolFirstClickLocation.y + 1f)
                        {
                            UseRectangleTool(Mathf.FloorToInt(tilePreview.transform.localScale.y), Mathf.FloorToInt(tilePreview.transform.localScale.x));
                        }
                        
                    }
                } 
                if(rectangleToolFirstClickLocation != new Vector3(-100f, -100f, 0f) &&(mousePos.x > rectangleToolFirstClickLocation.x + 1f && mousePos.y > rectangleToolFirstClickLocation.y + 1f))
                {
                    tilePreviewSR.color = Color.white;
                    tilePreview.transform.localScale = new Vector3(Mathf.Abs(rectangleToolFirstClickLocation.x - Mathf.Floor(mousePos.x)+0.5f)+2f, Mathf.Abs(rectangleToolFirstClickLocation.y - Mathf.Floor(mousePos.y)+0.5f)+2f, 1f);
                    tilePreview.transform.position = new Vector3((rectangleToolFirstClickLocation.x + Mathf.Floor(mousePos.x)+1f)/2f, (rectangleToolFirstClickLocation.y + Mathf.Floor(mousePos.y)+1f)/2f, 0);
                
                    if(Input.GetMouseButtonDown(0))
                    {
                        rectangleTool = false;
                        rectangleToolFirstClickLocation = new Vector3(-100f, -100f, 0f);
                        currentTile = null;
                        tilePreview.transform.localScale = new Vector3(1f, 1f, 1f);
                        GameObject.Find("Warning Text").GetComponent<Text>().text = "";
                    }
                   
                }
                else
                {
                    tilePreviewSR.color = Color.red;
                    if (rectangleToolFirstClickLocation != new Vector3(-100f, -100f, 0f))
                    {
                        tilePreview.transform.localScale = new Vector3(Mathf.Abs(rectangleToolFirstClickLocation.x - Mathf.Floor(mousePos.x) + 0.5f) + 2f, Mathf.Abs(rectangleToolFirstClickLocation.y - Mathf.Floor(mousePos.y) + 0.5f) + 2f, 1f);
                        tilePreview.transform.position = new Vector3((rectangleToolFirstClickLocation.x + Mathf.Floor(mousePos.x) + 1f) / 2f, (rectangleToolFirstClickLocation.y + Mathf.Floor(mousePos.y) + 1f) / 2f, 0);
                        GameObject.Find("Warning Text").GetComponent<Text>().text = "Move mouse up and right of your original click";
                    }
                   
                } 
                
                
                
                
                /*








                WORK HERE NEXT TIME








                */
            }
        }
        else
        {
            tilePreviewSR.sprite = null;
        }
        
        
        if(Input.GetKeyDown("escape"))
        {
            GameObject.Find("InfoMenuBackground").transform.position = new Vector3(-1000f, -1000f, 0f);
            info = false;
            currentTile = null;
            tilePreviewSR.sprite = null;
            delete=false;
            rectangleTool = false;
            rectangleToolFirstClickLocation = new Vector3(-100f, -100f, 0f);
            tilePreview.transform.localScale = new Vector3(1f, 1f, 1f);
                
        }

        if(Input.GetKeyDown("p"))
        {
            
        }
    }

    public void UseRectangleTool(int rows, int columns)
    {
        bool noXTiles = true;
        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < columns; c++)
            {
                if (tilemap1.GetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0)) == xTile)
                {
                    noXTiles = false;
                }
            }
        }
        if (noXTiles)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (r == 0)
                    {
                        if (c == 0)
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-11, -11, 0)));
                        }
                        else if (c == columns - 1)
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-9, -11, 0)));
                        }
                        else
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-10, -11, 0)));
                        }
                    }


                    else if (r == rows - 1)
                    {
                        if (c == 0)
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-11, -9, 0)));

                        }
                        else if (c == columns - 1)
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-9, -9, 0)));
                        }
                        else
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-10, -9, 0)));
                        }
                    }
                    else
                    {
                        if (c == 0)
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-11, -10, 0)));
                        }
                        else if (c == columns - 1)
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-9, -10, 0)));

                        }
                        else
                        {
                            tilemap1.SetTile(new Vector3Int(c + Mathf.FloorToInt(rectangleToolFirstClickLocation.x), r + Mathf.FloorToInt(rectangleToolFirstClickLocation.y), 0), tilemap1.GetTile(new Vector3Int(-10, -10, 0)));
                        }
                    }
                }
            }
        }
    }

    public void deleteXTiles()
    {
        for (int temp = 1; temp < tilemap.allTiles.Count; temp++)
        {
            if (tilemap1.GetTile(currentDeletionLocation) == tilemap.allTiles[temp] && tilemap1.GetTile(currentDeletionLocation) != null && tilemap1.GetTile(currentDeletionLocation) != xTile)
            {
                allTileCurrentNumbers[temp]--;
                for (int temp2 = 1; temp2 < (tilemap.allTileSizes[temp].x * tilemap.allTileSizes[temp].y); temp2++)
                {
                    tilemap1.SetTile(new Vector3Int(Mathf.FloorToInt(currentDeletionLocation.x + (temp2 % tilemap.allTileSizes[temp].x)), currentDeletionLocation.y + Mathf.FloorToInt(temp2 / tilemap.allTileSizes[temp].x), 0), null);
                }
            }
        }
    }
    public void printTilemapInformation(string levelName)
    {
        string fileName =  Application.streamingAssetsPath + "/" + levelName + ".txt";
        
        try
        { 
            using (StreamReader reader = new StreamReader(fileName))  
            {  
                columns = int.Parse(reader.ReadLine());
                rows = int.Parse(reader.ReadLine());
                levelInformation = reader.ReadLine();
            }  

        }
        catch
        {
            print("This file can't be read");
        }

        
        for(int k = 0; k < levelInformation.Length; k++)
        {
            for(int l = 0; l < allTileCharacters.Count; l++)
            {
                if (levelInformation.Substring(k, 1) == allTileCharacters[l])
                {
                    tilemap1.SetTile(new Vector3Int(k % columns, Mathf.FloorToInt(k/columns), 0), allTiles[l]);
                }
            }
        }
    }

    public string getTilemapInformation(int columns, int rows)
    {
        string tileInformation = "";
        
        for(int i = 0; i < (columns * rows); i++)
        {
            TileBase currentTile = tilemap1.GetTile(new Vector3Int(i % columns, Mathf.FloorToInt(i/columns), 0));
            for(int j = 0; j < allTiles.Count; j++)
            {
                if(currentTile == allTiles[j])
                {
                    tileInformation += allTileCharacters[j];
                }
                
            }
            if(currentTile == xTile)
            {
                tileInformation += "a";
            }
        }
        return(tileInformation);
    }

    public void buttonClicked(string name)
    {
        print("button clicked");
        for(int i = 0; i < allTileCharacters.Count; i++)
        {
            if(allTileCharacters[i] == name)
            {
                currentTile = allTiles[i];
                currentTileSize = allTileSizes[i];
                currentTileIndex = i;
                delete = false;
                rectangleTool = false;
            }
        }
        if(name == "delete")
        {
            delete = true;
            rectangleTool = false;
            currentTile = deleteTile;
            currentTileIndex = 0;
            tilePreviewSR.sprite = GameObject.Find("delete").GetComponent<Image>().sprite;
        }
        if(name == "rectangleTool")
        {
            rectangleTool = true;
            delete = false;
            currentTile = rectangleToolTile;
            currentTileIndex = 0;
            tilePreviewSR.sprite = GameObject.Find("rectangleTool").GetComponent<Image>().sprite;
        }
        if (name == "info")
        {
            info = true;
            rectangleTool = false;
            delete = false;
            currentTile = null;
            currentTileIndex = 0;
            tilePreviewSR.sprite = null;
            GameObject.Find("InfoMenuBackground").transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
            GameObject.Find("Warning Text").GetComponent<Text>().text = "";
        }
        else
        {
            info = false;
            GameObject.Find("InfoMenuBackground").transform.position = new Vector3(-1000f, -1000f, 0f);
        }
            
        if(name == "CreateLevelButton")
        {
            string nameOfLevel = levelNameText.text;
            string nameOfCreator = creatorNameText.text;
            if (nameOfLevel != "" && nameOfCreator != "" && winCondition.value != 0)
            {
                if(allTileCurrentNumbers[5] == 1)
                {
                    saveLevel(nameOfLevel, nameOfCreator);
                }
                else GameObject.Find("Warning Text").GetComponent<Text>().text = "Please place down the character";
            }
            else GameObject.Find("Warning Text").GetComponent<Text>().text = "Fill in both name of level and creator name, and select a win condition";
        }
    }

    public void saveLevel(string levelName, string creatorName)
    {
        bool fileExists = false;
        string fileName =  Application.streamingAssetsPath + "/" + levelName + ".txt";

        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach(FileInfo i in info)
        {
            if(File.Exists(fileName))
            {
                fileExists = true;
                GameObject.Find("Warning Text").GetComponent<Text>().text = "There is already a level named that. Please rename your level";
            }
        }

        if(!fileExists)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.OpenOrCreate); 
                using (StreamWriter writer = new StreamWriter(stream))  
                {  
                    writer.WriteLine(creatorName);
                    writer.WriteLine(winCondition.value);
                    writer.WriteLine(columns);
                    writer.WriteLine(rows);
                    writer.WriteLine(getTilemapInformation(columns, rows));  
                }  
                SceneManager.LoadScene("LevelSelection");
            }
            catch
            {
                print("L");
            }
        }
    }
}
