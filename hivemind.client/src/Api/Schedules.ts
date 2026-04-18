import type { QueryScheduleItem, Schedule, ScheduleItem } from '../Types/Schedule';

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
        body: JSON.stringify({ ...schedule }),
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

export const createScheduleItem = async (scheduleItem: ScheduleItem) => {
    return await fetch('/scheduleitems', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...scheduleItem }),
    });
}

export const updateScheduleItem = async (scheduleItem: ScheduleItem) => {
    return await fetch('/scheduleitems/' + scheduleItem.scheduleItemId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...scheduleItem }),
    });
}

export const createQueryScheduleItem = async (queryScheduleItem: QueryScheduleItem) => {
    const response = await fetch('/queryscheduleitems', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...queryScheduleItem }),
    });

    return await response.json();
}

export const deleteQueryScheduleItem = async (id: number) => {
    return await fetch('/queryscheduleitems/' + id, {
        method: 'Delete',
        headers: { 'Content-Type': 'application/json' }
    });
}

export const fetchScheduleItem = async (id: number): Promise<ScheduleItem> => {
    const response = await fetch('/scheduleitems/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch schedule Item: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    return data;
};

export const fetchQueryScheduleItemsByScheduleItem = async (id: number) => {
    const response = await fetch('/queryscheduleitems/scheduleItem/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch query schedule items: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.items;
}