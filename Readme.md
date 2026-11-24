# HiveMind
Under construction.

# Things to do:

## Front end
1. Add by library filter param to media page
2. Add category filter to tags page

## Singal R implementation for Hosted Services
1. Scheduler should display a notification when scheduling?

## Scheduler
1. Shuffle Play
    - play all items from a query non-duplicative, before playing randlomly
    - Needs to be per schedule? (program strat)
2. Iterative play (Reruns, Season Schedules)
    - Should be date based... so that if a schedule is regenerated it does not skip to the next one...
3. Fix Generic Bumps as fallback...
4. Figure out how Up Next should work...

eventually pull a random schedule from http://cnas.fandom.com/wiki/January_2,_2000
and play the whole thing... I would have to figure out how to create blocks programmatically...

## Lineups
1. Visual Editor
2. JSON validation
3. Example Lineup or a library of nodes

Apply lineup change to current schedules...?

## Commerical Podding
1. Define and use pods as queries for filler... or is this just a set of tags?...

## Daypart management
1. Need to define day part timings.
    - morning, afternoon, primetime, overnight
    - is this per station? probably...

## Integrations
1. ErsaztTV Next (create the schedule need), add it as drone type?, Need to create an EPG generator for this

## Current Issues & Needed Upgrades
1. If our services error for any reason the stop with no restart strat...
    Need to implement a backoff strategy that turns off what ever program strategy... and errors the db entries
2. Form Validiation is lacking multiple spots
3. Testing for Schedule Retrieval



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

### Truenas Deployment Yaml

```
services:
  hivemind:
    container_name: hivemind
    environment:
      - PUID=568
      - PGID=568
      - TZ=America/New_York
    image: >-
      ghcr.io/stevenmdixon/hivemind@sha256:42b24d0fac554aba311445b5abb9d8fb2b3b5535119be4fde8ea347714c6b394
    ports:
      - '8900:8080'
      - '8901:8081'
    restart: unless-stopped
    user: '568:568'
    volumes:
      - /mnt/General/hivemind2/config:/config
      - /mnt/General/Media/media_us:/media
version: '3.8'
```