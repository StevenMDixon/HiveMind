# HiveMind
Under construction.

# Things to do:

## Front end
1. Add by library filter param to media page
2. Add category filter to tags page


## Singal R implementation for Hosted Services

## Remove Programming block as a required tag

## Scheduler
1. Shuffle Play
    - play all items from a query non-duplicative, before playing randlomly
    - Needs to be per schedule? (program strat + lineup?)
2. Iterative play (Reruns, Season Schedules)
    - Should be date based... so that if a schedule is regenerated it does not skip to the next one...
3. EPG Generation
4. Fix Generic Bumps?

eventually pull a random schedule from http://cnas.fandom.com/wiki/January_2,_2000
and play the whole thing... I would have to figure out how to create blocks programmatically...

## Lineups
1. Visual Editor
2. JSON validation
3. Example Lineup or a library of node types

Apply lineup change to current schedules...

## Media Importer
1. Pull metadata from files into tags
2. Library Id Queries by default

## Program Strategy
1. Weekday & Weekend types for schedule

## Commerical Podding
1. Define and use pods as queries for filler... or is this just a set of tags...

## Daypart management
1. Need to define day part timings.
    - morning, afternoon, primetime, overnight
    - is this per station? probably...


## Integrations
1. ErsaztTV Next (create the schedule need), add it as drone type?


# Running locally

Run client
```
npm install
npm run dev
```

Run server
```
docker compose up 
or 
docker compose up -d
```

### Updating ef-core db migrations

```
sudo chown -R "$USER":"$(id -gn)"   HiveMind.Server/bin HiveMind.Server/obj
dotnet build HiveMind.Server/HiveMind.Server.csproj
dotnet ef migrations add NameOfUpdate
dotnet ef database update
```

### Script
```
services:
  hivemind:
    container_name: hivemind
    environment:
      - PUID=568
      - PGID=568
      - TZ=America/New_York
    image: >-
      ghcr.io/stevenmdixon/hivemind@sha256:40db573d9c38ca0f49158a050ace5381e37727528dabb85f94e74dc459f84870
    ports:
      - '8080:8080'
      - '8081:8081'
    restart: unless-stopped
    volumes:
      - /mnt/General/hive/config:/config:rw
version: '3.8'
```