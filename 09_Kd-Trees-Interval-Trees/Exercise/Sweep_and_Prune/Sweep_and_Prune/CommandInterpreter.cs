using System.Collections.Generic;

class CommandInterpreter
{
    private List<GameObject> objects;

    private Dictionary<string, GameObject> objectsName;

    private int ticks;

    private bool isGameStarted;

    public CommandInterpreter()
    {
        this.objects = new List<GameObject>();
        this.objectsName = new Dictionary<string, GameObject>();
    }

    public int Ticks
    {
        get
        {
            return this.ticks;
        }
    }

    public void StartGame()
    {
        this.isGameStarted = true;
    }

    public void Tick()
    {
        if (this.isGameStarted)
        {
            this.ticks = this.Ticks + 1;
        }
    }

    public void AddObject(string name, int x1, int y1)
    {
        var newObject = new GameObject(name, x1, y1);
        this.objects.Add(newObject);
        this.objectsName.Add(name, newObject);
    }

    public void MoveObject(string name, int newX1, int newY1)
    {
        if (this.isGameStarted)
        {
            this.objectsName[name].X1 = newX1;
            this.objectsName[name].Y1 = newY1;
            this.ticks++;
        }
    }

    public List<GameObject> SweepAndPrune()
    {
        var resultObjects = new List<GameObject>();
        this.InsertionSort();
        for (int i = 0; i < this.objects.Count; i++)
        {
            var currentObj = this.objects[i];
            for (int j = i + 1; j < this.objects.Count; j++)
            {
                var candidateCollisionObj = this.objects[j];
                if (currentObj.X2 < candidateCollisionObj.X1)
                {
                    break;
                }

                if (currentObj.Intersects(candidateCollisionObj))
                {
                    resultObjects.Add(currentObj);
                    resultObjects.Add(candidateCollisionObj);
                }
            }
        }

        return resultObjects;
    }

    private void InsertionSort()
    {
        for (int i = 0; i < this.objects.Count - 1; i++)
        {
            int j = i + 1;
            while (j > 0)
            {
                if (this.objects[j - 1].X1 > this.objects[j].X1)
                {
                    var temp = this.objects[j - 1];
                    this.objects[j - 1] = this.objects[j];
                    this.objects[j] = temp;
                }

                j--;
            }
        }
    }
}