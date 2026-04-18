export interface ScheduleItem {
    scheduleItemId: number;
    name: string;
    index: number;
    type: string;
    queries: QueryScheduleItem[];
}

export interface QueryScheduleItem {
    queryScheduleItemId: number;
    queryId: number;
    scheduleItemId: number;
    queryType: number;
    index: number;
    padTo: number;
    playCount: number;
    playDuration: number;
    playoutType: number;
}

export interface Schedule {
    scheduleId: number;
    scheduleName: string;
    channelId: number | null;
    startTime: string;
    scheduleItems: ScheduleItem[];
}