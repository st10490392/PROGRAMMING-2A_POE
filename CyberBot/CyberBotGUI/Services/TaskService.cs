using System.Collections.Generic;
using CyberBotGUI.Models;

namespace CyberBotGUI.Services;

public class TaskService
{
    private List<TaskItem> tasks = new();
    private int nextId = 1;

    public List<TaskItem> GetTasks()
    {
        return tasks;
    }

    public void AddTask(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return;

        tasks.Add(new TaskItem
        {
            Id = nextId++,
            Description = description,
            Completed = false
        });
    }

    public void DeleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);
        }
    }

    public void CompleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].Completed = !tasks[index].Completed;
        }
    }
}