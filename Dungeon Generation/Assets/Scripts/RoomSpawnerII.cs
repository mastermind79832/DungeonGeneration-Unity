using UnityEngine;

public class RoomSpawnerII : MonoBehaviour
{
    [SerializeField]private GameObject roomPrefab;
    private const int ROOM_SIZE = 20;
    [SerializeField] private int roomCountX;
    [SerializeField] private int roomCountY;

    private RoomScript[,] rooms;
    [SerializeReference] private int[,] opening;
    // Start is called before the first frame update
    void Awake()
    { 
        opening = new int[roomCountX,roomCountY];
        CreateRooms();   
        DeleteAllRoomWalls();
    }

    private void DeleteAllRoomWalls()
    {
        for(int x =0; x<roomCountX; x++)
            for(int y=0; y<roomCountY; y++)
            {
                if(rooms[x,y] != null) 
                    rooms[x,y].DeleteWalls(opening[x,y]);
            }
    }

    private void CreateRooms()
    { 
        rooms = new RoomScript[roomCountX,roomCountY];
        rooms[0,0]= CreateSingleRoom(0,0);

        int x=0;
        int y=0;
        while(y<roomCountY)
        {
            if(rooms[x,y]==null)
            {
                rooms[x,y]= CreateSingleRoom(x,y);
            }

            if(Random.Range(0,10) >= 7)
                x += Random.Range(-1,2);
            else
                y += Random.Range(-1,2);

            if(x<0)
                x=0;
            if(y<0)
                y=0;
           
            if(x>=roomCountX)
            {    
                x--;
                y++;
            }
        }


        for(x=0; x< roomCountX; x++)
            for (y = 0; y < roomCountY; y++)
                if(rooms[x,y]!= null)
                    opening[x,y] = CreateOpeining(rooms,x,y);   
      
    }

    private RoomScript CreateSingleRoom(int x, int y)
    {
        Vector2 pos = new Vector2(x,y) *ROOM_SIZE;
        GameObject room = Instantiate(roomPrefab,pos,Quaternion.identity);
        room.name = string.Format("X:{0} ; Y:{1}",x,y);
        room.transform.parent = transform;     
        return room.GetComponent<RoomScript>();
       
    }

    private int CreateOpeining(RoomScript[,] rooms,int x, int y)
    {
        int open=0;
        
        if(y+1 < roomCountY) // top
        {
            if( rooms[x,y+1] != null)
            {
                open += 1000;
            }
        } 
        
        if(x+1 < roomCountX) // right
        {
            if( rooms[x+1,y] != null)
            {
                open += 100;
            }
        }

        if(y-1 >= 0)    // bottom
        {
            if( rooms[x,y-1] != null)
            {
                open += 10;
            }
        }

        if(x-1 >= 0) //left
        {
            if( rooms[x-1,y] != null)
            {
                open += 1;
            }
        }

        return open;
    }



}
