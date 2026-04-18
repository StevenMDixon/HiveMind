import { fetchChannel, fetchChannels, saveChannel, createChannel, deleteChannel } from './Channel'
import { fetchQueryTypes, fetchOptions, fetchPlayoutTypes, fetchSettings } from './Enums';
import { fetchQuery, fetchQueries, deleteQuery, createQuery, saveQuery, fetchTestQueryResults } from './Queries';
import { fetchLibrary, saveLibrary, deleteLibrary, fetchLibraries, createLibrary, fetchLibraryTypes } from './Libraries'
import {
    fetchScheduleItem, deleteSchedule, deleteQueryScheduleItem, createSchedule, createScheduleItem, fetchSchedules, fetchScheduleData,
    fetchQueryScheduleItemsByScheduleItem, createQueryScheduleItem,
    updateScheduleItem,
    updateSchedule
} from './Schedules';
import { fetchMedia } from './Media';

export const ApiClient = {
    //channels
    fetchChannel,
    fetchChannels,
    saveChannel,
    createChannel,
    deleteChannel,

    //enums
    fetchQueryTypes,
    fetchOptions,
    fetchPlayoutTypes,
    fetchSettings,

    //queries
    fetchQuery,
    createQuery,
    fetchQueries,
    deleteQuery,
    saveQuery,
    fetchTestQueryResults,

    //library
    fetchLibrary,
    fetchLibraries,
    deleteLibrary,
    saveLibrary,
    createLibrary,
    fetchLibraryTypes,

    //schedules
    fetchScheduleData,
    fetchSchedules,
    createSchedule,
    deleteSchedule,
    updateSchedule,
    createScheduleItem,
    fetchScheduleItem,
    updateScheduleItem,

    createQueryScheduleItem,
    deleteQueryScheduleItem,
    fetchQueryScheduleItemsByScheduleItem,

    //media
    fetchMedia
};