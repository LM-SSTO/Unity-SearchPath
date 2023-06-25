namespace Script.SearchPath
{
    
    
    
    public class Node
    {
        public int X { get; set; } 
        public int Y { get; set; } 
        public int GridType { get; set; } 
        public int F { get { return G + H; } } 
        public int G { get; set; } // 从起点到当前节点的路径已走过的距离
        public int H { get; set; } // 从当前节点到终点的估算距离
        
        public Node Parent { get; set; } // 当前节点在路径中的父节点

        public Node(int x, int y, int gridType)
        {
            X = x;
            Y = y;
            GridType = gridType;
        }
    }
}