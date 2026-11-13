import { Route, Routes, HashRouter } from 'react-router';
import './App.css';
import DashboardLayout from './Dashboard/layout';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

import Intro from './Pages/Intro';

import StationPage from './features/stations/pages/Stations';
import StationEdit from './features/stations/pages/StationEdit';
import LibaryPage from './features/libraries/pages/Library';
import LibraryEdit from './features/libraries/pages/LibraryEdit';
import MediaPage from './features/media/pages/Media'
import QueryPage from './features/queries/pages/Queries'
import QueryDetail from './features/queries/pages/QueryDetail'
import LineupView from './features/lineups/pages/Lineup'
import LineupEdit from './features/lineups/pages/LineupEdit'
import ShowsPage from './features/shows/pages/Shows';
import ShowsEdit from './features/shows/pages/ShowEdit';
import SettingsPage from './features/settings/pages/Settings';
import ProgramStrategyPage from './features/programStrategy/pages/ProgramStrategy';
import ProgramStrategyDetail from './features/programStrategy/pages/ProgramStrategyDetail';
import DronePage from './features/drones/pages/Drones';
import DroneEdit from './features/drones/pages/DroneEdit';
import TransitionPage from './features/transitions/pages/Transitions';
import TransitionEdit from './features/transitions/pages/TransitionEdit';
import TagPage from './features/tags/pages/Tags';
import QueuePage from './features/queue/pages/queue';
import MediaTagging from './features/queue/pages/media';

const darkTheme = createTheme({
    palette: {
        mode: 'dark',
        secondary: {
            main: '#ffc400',
            dark: '#ffc400',
        }
    },
});


const App = () => {

    const queryClient = new QueryClient();
    return (
        <QueryClientProvider client={queryClient}>
        <ThemeProvider theme={darkTheme}>
            <CssBaseline />
                <HashRouter>
                <Routes>
                    <Route path="/" element={<DashboardLayout />}>
                        <Route index element={<Intro />} />
                            <Route path="stations" element={<StationPage />} />
                            <Route path="stations/:id/" element={<StationEdit />} />
                            <Route path="lineups" element={<LineupView />} />
                            <Route path="lineups/:id" element={<LineupEdit />} />
                            {/* <Route path="lineups/:id/items/:itemId" element={<LineupItemEdit />} /> */}
                            <Route path="queries" element={<QueryPage />} />
                            <Route path="queries/:id" element={<QueryDetail />} />
                            <Route path="libraries" element={<LibaryPage />} />
                            <Route path="libraries/:id" element={<LibraryEdit />} />
                            <Route path="media" element={<MediaPage />} />
                            {/*<Route path="media/manage/:id" element={<p>Hello!</p>} />*/}
                            <Route path="shows" element={<ShowsPage />} />
                            <Route path="shows/:id" element={<ShowsEdit />} />
                            <Route path="system-settings" element={<SettingsPage />} />
                            <Route path="programstrategy/" element={<ProgramStrategyPage />} />
                            <Route path="programstrategy/:id" element={<ProgramStrategyDetail />} />
                            <Route path="drones" element={<DronePage />} />
                            <Route path="drones/:id" element={<DroneEdit />} />
                            <Route path="transitions" element={<TransitionPage />} />
                            <Route path="transitions/:id" element={<TransitionEdit />} />
                            <Route path="tags" element={<TagPage />} />
                            <Route path="queue" element={<QueuePage />} />
                            <Route path="queue/:id" element={<MediaTagging />} />
                        </Route>
                </Routes>
                </HashRouter>
            </ThemeProvider>
        </QueryClientProvider>
    )
}

export default App;