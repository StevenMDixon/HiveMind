export interface ScheduleItem {
    scheduleItemId: number;
    name: string;
    index: number;
    type: string;
}

export interface Schedule {
    scheduleId: number;
    scheduleName: string;
    scheduleItems: ScheduleItem[];
}