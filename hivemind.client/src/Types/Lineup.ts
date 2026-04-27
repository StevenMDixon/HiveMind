export interface LineupItem {
    lineupItemId: number;
    name: string;
    index: number;
    type: string;
    queries: QueryLineupItem[];
}

export interface QueryLineupItem {
    queryLineupItemId: number;
    queryId: number;
    lineupItemId: number;
    queryType: number;
    index: number;
    padTo: number;
    playCount: number;
    playDuration: number;
    playoutType: number;
}

export interface Lineup {
    lineupId: number;
    lineupName: string;
    channelId: number | null;
    startTime: string;
    lineupItems: LineupItem[];
}