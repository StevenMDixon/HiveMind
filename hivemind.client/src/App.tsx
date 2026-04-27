import { Route, Routes, HashRouter } from 'react-router';
import './App.css';
import DashboardLayout from './Dashboard/layout';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

import Intro from './Pages/Intro';

import ChannelPage from './Pages/Channels/Channels';
import LibaryPage from './Pages/Libraries/Library';
import QueryPage from './Pages/Queries/Queries'
import MediaPage from './Pages/Media/MediaView'
import QueryDetail from './Pages/Queries/QueryDetail'
import LineupView from './Pages/Lineups/Lineup'
import LineupEdit from './Pages/Lineups/LineupEdit'
import LineupItemEdit from './Pages/Lineups/LineupItem/LineupItemEdit'; 
import ChannelEdit from './Pages/Channels/ChannelEdit';
import LibraryEdit from './Pages/Libraries/LibraryEdit';
import ShowsPage from './Pages/Shows/ShowsView';

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
    return (
        <ThemeProvider theme={darkTheme}>
            <CssBaseline />
                <HashRouter>
                <Routes>
                    <Route path="/" element={<DashboardLayout />}>
                        <Route index element={<Intro />} />
                            <Route path="channels" element={<ChannelPage />} />
                            <Route path="channels/:id/" element={<ChannelEdit />} />
                            <Route path="lineups" element={<LineupView />} />
                            <Route path="lineups/:id" element={<LineupEdit />} />
                            <Route path="lineups/:id/items/:itemId" element={<LineupItemEdit />} />
                            <Route path="queries" element={<QueryPage />} />
                            <Route path="queries/:id" element={<QueryDetail />} />
                            <Route path="libraries" element={<LibaryPage />} />
                            <Route path="libraries/:id" element={<LibraryEdit />} />
                            <Route path="media" element={<MediaPage />} />
                            <Route path="media/manage/:id" element={<p>Hello!</p>} />
                            <Route path="shows" element={<ShowsPage />} />
                        </Route>
                </Routes>
                </HashRouter>
        </ThemeProvider>
    )
}

export default App;