export interface ScheduleItem {
    scheduleItemId: number;
    name: string;
    index: number;
    type: string;
    collections: CollectionScheduleItem[];
}

export interface CollectionScheduleItem {
    collectionScheduleItemId: number;
    collectionId: number;
    collectionType: number;
    index: number;
    padTo: number;
    playCount: number;
    playDuration: number;
    playoutType: number;
    scheduleItemId: number;
}

export interface Schedule {
    scheduleId: number;
    scheduleName: string;
    channelId: number | null;
    startTime: string;
    scheduleItems: ScheduleItem[];
}