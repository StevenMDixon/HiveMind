export interface ScheduleItem {
    scheduleItemId: number;
    name: string;
    index: number;
    type: string;
    collections: CollectionScheduleItem[];
}

export interface CollectionScheduleItem {
    collectionScheduleItemId: number;
}

export interface Schedule {
    scheduleId: number;
    scheduleName: string;
    channelId: number | null;
    startTime: string;
    scheduleItems: ScheduleItem[];
}