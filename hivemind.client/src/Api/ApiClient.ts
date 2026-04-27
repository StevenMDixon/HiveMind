import { fetchChannel, fetchChannels, saveChannel, createChannel, deleteChannel } from './Channel'
import { fetchQueryTypes, fetchOptions, fetchPlayoutTypes, fetchSettings, fetchPadToOptions } from './Enums';
import { fetchQuery, fetchQueries, deleteQuery, createQuery, saveQuery, fetchTestQueryResults } from './Queries';
import { fetchLibrary, saveLibrary, deleteLibrary, fetchLibraries, createLibrary, fetchLibraryTypes, reprocessLibrary } from './Libraries'
import {
    fetchLineup, fetchLineups, createLineup, updateLineup, deleteLineup, createLineupItem, updateLineupItem, fetchLineupItem,
    createQueryLineupItem, deleteQueryLineupItem, fetchQueryLineupItemsByLineupItem
} from './Lineup';
import { fetchMedia } from './Media';
import { fetchShows } from './Shows'

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
    fetchPadToOptions,

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
    reprocessLibrary,

    //lineups
    fetchLineup,
    fetchLineups,
    createLineup,
    updateLineup,
    deleteLineup,
    createLineupItem,
    fetchLineupItem,
    updateLineupItem,
    createQueryLineupItem,
    deleteQueryLineupItem,
    fetchQueryLineupItemsByLineupItem,

    //media
    fetchMedia,
    //shows
    fetchShows
};