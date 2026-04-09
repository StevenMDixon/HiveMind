import type { Schedule } from '../Types/Schedule';

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