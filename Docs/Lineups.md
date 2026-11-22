# Lineups

Lineups are a collection of nodes that work together to define a scheduling template.

## Nodes

A lineup `node` is categorized by type

### Container Nodes

1. Block
2. Program

### Block Node
The backbone of the scheduler a block node defines the sources and transistion types
the slots will use when generating the schedule

```
{
    "nodeType": "Block",
    "name": "",
    "logo": "url" 
    "sources" : [],
    "transitions: {},
    "slots": [nodes]
}
```

### Program Node
A program node defines:
1. the duration of playback
2. The name of the program, this will be the name of the section 
3. useBlockName tells the breaks to add the blocks name to any search tags for media
4. Slot can be any data or data manipulation nodes

```
{
    "nodeType": "Program",
    "programName": "",
    "duration": 30min,
    "useBlockName": false
    "slot": node
}
```

### Data Nodes

1. MediaQuery
2. MediaServer

### MediaQuery Node

```
{
    "nodeType": "MediaQuery",
    "retrievalSetting": "Random",
    "query" : {
        "type": id | name | custom,
        "value": ""
        id = query id,
        name = query name,
        custom = a custom query of ["Title", "Equals", "Something"]
    }
}
```

### MediaServer Node

```
{
    "nodeType": "MediaServer",
    "duration": in ms,
    "url": url to pull
}
```
If using mpv as a playback if you specify a youtube video, you will have install ytdlp-deno?
the playback duration may not match the videos duration if it takes a while to load.

### Special Nodes

1. Break

### Break Node
takes a single node, but can be used in conjunction with the Repeat or splitter Node
This node handles the heavy lifting of placing transitions and filler between segments
This will pad to the duration specified by a program node.

```
{
    "nodeType": "Break",
    "slot": node
}
```

### Data Manipulation Nodes

1. Repeat
2. Splitter

### Repeater Node
This node repats a node for a specified number of times

```
{
    "nodeType": "Repeater",
    "slot": node
}
```

### Splitter Node
This node splits a data node using a split strategy

```
{
    "nodeType": "Splitter",
    "splitStrategy": Every | ShowDefined | FixedCount,
    "SplitInterval": time in ms
}
```