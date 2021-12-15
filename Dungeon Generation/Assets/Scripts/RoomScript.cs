using UnityEngine;

public class RoomScript : MonoBehaviour
{

    private Vector2Int location = new Vector2Int();

    [SerializeField] private GameObject[] walls;

    public int opens;

    public Vector2Int GetLocation(){ return location ;}
    public void SetLocation( Vector2Int _location) {location = _location;}
    
    public void DeleteWalls(int open)
    {    
        opens= open;
        if(open >= 1000)
        {
            Destroy(walls[0]);
            walls[0] = null;
        }

        if(open%1000 >= 100)
        {
            Destroy(walls[1]);
            walls[1] = null;
        }

        if(open%100 >= 10)
        {
            Destroy(walls[2]);
            walls[2] = null;
        }

        if(open%2 == 1)
        {
            Destroy(walls[3]);
            walls[3] = null;
        }
    }
}
