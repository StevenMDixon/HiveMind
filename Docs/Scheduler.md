
# Program Strategy

A Program Strategy Pulls together a collection of scheduling rules, it defines:
1. When to use specific lineups
2. Promos for special events
3. Rotation schedules for shows

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
    "nodeType": "block",
    "name": "",
    "logo": "url" 
    "sources" : [list of data source items],
    "transitions: {},
    "segments": [list of segment nodes]
}
```

### Segment Node
A Segment node defines a piece of a block, this is usaully limited to one media Item,
Segments can provide their own sources that will be placed over the blocks.
Segments can also provide their own transitions allowing them to have custom mid and post transistions.
This node provides its own duration, that it will take up. Unused time by the selected media items will be filled by filler.

splitConfig is optional and allows for a media Item to be broken into smaller parts so that breaks can be placed.
```
{
    "nodeType": "segment",
    "name": "",
    "duration": 30, // this is in minutes
    "useParentName": false // adds the parent name as part of queries
    "dataRetriever": see DataRetrievers,
    "sources" : [list of data source items],
    "transitions": { },
    "splitConfig": {"splitStrategy": "FixedCount", "splitInterval": 0}
}
```

### DataRetrievers
These tell a segment how to retrieve its media Item for playback.
By default the Segments Name is passed into these.

1. QueryRetriever
2. RotationRetriever
3. ServerRetriever
4. ShowRetriever

#### QueryRetriever
This Data Retreiver uses a IDataSource as an extremely flexible query
```
{
    "type": "query",
    "query": {IDataSource}
}
```

#### RotationRetriever
This Data Retreiever uses a specific show name to match and rotation defined on the program strategy.
This allows for sequential playback of items and reruns.
```
{
    "showName": ""
}
```

#### ServerRetriever
This Data Retreiever allows the user to specify a URL to play media from an online source.
A Duration (in minutes) must also be specified so that the scheduler can add filler if needed.
Playback of this item does not start when media playback starts.
```
{
    "url": "",
    "duration": 10
}
```
#### ShowRetriever
This Data Retreiever allows the user to specify a show to retrieve Items from.
Optionally the Season and Episode numbers can be provided to narrow the retrieved items.
```
{
    "showName": "",
    "season": ?,
    "episode": ?
}
```

### DataSource
A data source is a flexible way of specifying what queries to use when pulling data
There are a couple types:

1. Custom
2. QueryId
3. QueryName

#### Custom
The custom data source allows users to make up a query on the fly.

```
{
    "type": "custom",
    "queries": ["Title, Equals, Pokemon", "Season, GreaterThan, 1"]
}
```

### QueryId
This data source allows a user to specify the Id of a query
```
{
    "type": "id",
    "id": 111
}
```

### QueryName
This data source allows a user to specify the Name of a query
```
{
    "type": "name",
    "name": ""
}
```