import type { CollectionScheduleItem, Schedule, ScheduleItem } from '../Types/Schedule';

export const fetchScheduleData = async (id: number) => {
    const response = await fetch('/schedules/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch schedule: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
}

export const updateSchedule = async (schedule: Schedule) => {
    const result = await fetch('/schedules/' + schedule.scheduleId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ScheduleName: schedule.scheduleName, ScheduleItems: schedule.scheduleItems }),
    });

    return result;
}

export const fetchSchedules = async (): Promise<Schedule[]> => {
    const response = await fetch('/schedules');

    if (!response.ok) {
        throw new Error(`Failed to fetch schedules: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.schedules;
};

export const createSchedule = async (schedule: Schedule) => {
    return await fetch('/schedules', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ScheduleName: schedule.scheduleName, ChannelId: schedule.channelId }),
    });
}

export const deleteSchedule = async (schedule: Schedule) => {
    return await fetch('/schedules/' + schedule.scheduleId, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });
}

export const createScheduleCollectionItem = async (collectionScheduleItem: CollectionScheduleItem) => {
    return await fetch('/collectionscheduleitem', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...collectionScheduleItem }),
    });
}

export const createScheduleItem = async (scheduleItem: ScheduleItem) => {
    const response = await fetch('/scheduleitems', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...scheduleItem }),
    });

    return await response.json();
}

export const updateScheduleItem = async (scheduleItem: ScheduleItem) => {
    const response = await fetch('/scheduleitems/' + scheduleItem.scheduleItemId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...scheduleItem }),
    });

    return response;
}

export const createCollectionScheduleItem = async (collectionScheduleItem: CollectionScheduleItem) => {
    const response = await fetch('/collectionscheduleitems', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...collectionScheduleItem }),
    });

    return await response.json();
}

export const deleteCollectionScheduleItem = async (id: number) => {
    return await fetch('/collectionscheduleitems/' + id, {
        method: 'Delete',
        headers: { 'Content-Type': 'application/json' }
    });
}

export const fetchCollectionScheduleItemsByScheduleItem = async (id: number) => {
    const response = await fetch('/collectionscheduleitems/scheduleItem/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch schedule: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.items;
}