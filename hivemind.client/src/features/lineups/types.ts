export interface LineupItem {
    id: number;
    name: string;
    index: number;
    type: string;
    queries: QueryLineupItem[];
}

export interface QueryLineupItem {
    id: number;
    queryId: number;
    lineupItemId: number;
    queryType: number;
    index: number;
    padTo: number;
    playCount: number;
    playDuration: number;
    playoutType: number;
    group: number;
}

export interface Lineup {
    id: number;
    name: string;
    channelId: number | null;
    startTime: string;
    jsonData: string;
}