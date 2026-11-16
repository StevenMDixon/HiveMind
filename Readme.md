# HiveMind
Under construction.

# Things to do:

## Scheduler
1. Shuffle Play 
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

## Media Importer
1. Pull metadata from files into tags
2. Library Id Queries by default

## Program Strategy
1. Weekday & Weekend types

## Commerical Podding
1. Define and use pods as queries for filler... or is this just a set of tags...

## Daypart management
1. Need to define day part timings.
    - morning, afternoon, primetime, overnight
    - is this per station? probably...

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

