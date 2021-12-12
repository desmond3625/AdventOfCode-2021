using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Solution
    {
        public Dictionary<string, List<string>> _graph = new();
        public int PathsCount { get; set; }
        public Dictionary<string, Boolean> _visited = new();

        public void CreateGraph(string[] input)
        {
            foreach (var line in input)
                AddEdge(line);
        }

        public void PrintGraph()
        {
            foreach (var vertex in _graph)
            {
                Console.Write(vertex.Key + ": ");
                foreach (var neighbour in vertex.Value)
                    Console.Write(neighbour + ", ");

                Console.WriteLine();
            }
        }

        public void AddEdge(string edge)
        {
            string[] vertices = edge.Split("-");
            string v1 = vertices[0];
            string v2 = vertices[1];

            foreach (var v in new List<string> {v1, v2})
            {
                if (!_visited.ContainsKey(v))
                    _visited.Add(v, false);
            }

            if (_graph.ContainsKey(v1))
            {
                if (!_graph[v1].Contains(v2))
                    _graph[v1].Add(v2);
            }
            else
                _graph.Add(v1, new List<string> {v2});

            if (_graph.ContainsKey(v2))
            {
                if (!_graph[v2].Contains(v1))
                    _graph[v2].Add(v1);
            }
            else
                _graph.Add(v2, new List<string> {v1});
        }
        
        public Boolean ValidatePath(List<string> path)
        {
            path = path.Where(x => Char.IsLower(x[0])).ToList();


            var g = path.GroupBy(i => i)
                .OrderByDescending(group => group.Count()).ToList();
            if (g.Count >= 2)
            {
                if (g[0].Count() > 2)
                    return false;

                if (g[0].Count() >= 2 && g[1].Count() >= 2)
                    return false;
            }

            return true;
        }

        public int FindAllPaths(string type)
        {
            PathsCount = 0;
            List<string> path = new List<string>();
            path.Add("start");

            if (type.Equals("part1"))
                FindAllPathsRecPart1("start", "end", path);

            else if (type.Equals("part2"))
                FindAllPathsRecPart2("start", "end", path);

            return PathsCount;
        }

        private void FindAllPathsRecPart1(string v1, string v2, List<string> path)
        {
            if (v1.Equals(v2))
            {
                PathsCount++;
                return;
            }

            if (char.IsLower(v1[0]))
                _visited[v1] = true;

            foreach (var i in _graph[v1])
            {
                if (!_visited[i])
                {
                    path.Add(i);
                    FindAllPathsRecPart1(i, v2, path);
                    path.Remove(i);
                }
            }

            _visited[v1] = false;
        }
        
        private void FindAllPathsRecPart2(string v1, string v2, List<string> path)
        {
            if (!ValidatePath(path))
                return;

            if (v1.Equals(v2))
            {
                PathsCount++;
                return;
            }

            foreach (var i in _graph[v1])
            {
                if (!i.Equals("start"))
                {
                    path.Add(i);
                    FindAllPathsRecPart2(i, v2, path);
                    path.Remove(i);
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day12\\day12.txt");

            Solution s = new Solution();
            s.CreateGraph(input);
            Console.WriteLine(s.FindAllPaths("part1"));
            Console.WriteLine(s.FindAllPaths("part2"));
        }
    }
}