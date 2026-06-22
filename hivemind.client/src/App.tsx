import { Route, Routes, HashRouter } from 'react-router';
import './App.css';
import DashboardLayout from './dashboard/layout';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

import Intro from './pages/Intro';

import StationPage from './features/stations/pages/Stations';
import StationEdit from './features/stations/pages/StationEdit';
import LibaryPage from './features/libraries/pages/Library';
import LibraryEdit from './features/libraries/pages/LibraryEdit';
import MediaPage from './features/media/pages/Media'
import QueryPage from './features/queries/pages/Queries'
import QueryDetail from './features/queries/pages/QueryDetail'
import LineupView from './features/lineups/pages/Lineup'
import LineupEdit from './features/lineups/pages/LineupEdit'
import LineupItemEdit from './features/lineups/pages/LineupItemEdit'; 
import ShowsPage from './features/shows/pages/Shows';
import SettingsPage from './features/settings/pages/Settings';
import ProgramStrategyPage from './features/programStrategy/pages/ProgramStrategy';
import ProgramStrategyDetail from './features/programStrategy/pages/ProgramStrategyDetail';

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
                            <Route path="lineups/:id/items/:itemId" element={<LineupItemEdit />} />
                            <Route path="queries" element={<QueryPage />} />
                            <Route path="queries/:id" element={<QueryDetail />} />
                            <Route path="libraries" element={<LibaryPage />} />
                            <Route path="libraries/:id" element={<LibraryEdit />} />
                            <Route path="media" element={<MediaPage />} />
                            {/*<Route path="media/manage/:id" element={<p>Hello!</p>} />*/}
                            <Route path="shows" element={<ShowsPage />} />
                            <Route path="system-settings" element={<SettingsPage />} />
                            <Route path="programstrategy/" element={<ProgramStrategyPage />} />
                            <Route path="programstrategy/:id" element={<ProgramStrategyDetail />} />
                        </Route>
                </Routes>
                </HashRouter>
            </ThemeProvider>
        </QueryClientProvider>
    )
}

export default App;