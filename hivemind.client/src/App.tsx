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
import Schedules from './Pages/Schedules/Schedules'
import ScheduleEdit from './Pages/Schedules/ScheduleEdit'
import ScheduleItemEdit from './Pages/Schedules/ScheduleItem/ScheduleItemEdit'; 
import ChannelEdit from './Pages/Channels/ChannelEdit';
import LibraryEdit from './Pages/Libraries/LibraryEdit';

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
                            <Route path="schedules" element={<Schedules />} />
                            <Route path="schedules/:id" element={<ScheduleEdit />} />
                            <Route path="schedules/:id/items/:itemId" element={<ScheduleItemEdit />} />
                            <Route path="queries" element={<QueryPage />} />
                            <Route path="queries/:id" element={<QueryDetail />} />
                            <Route path="libraries" element={<LibaryPage />} />
                            <Route path="libraries/:id" element={<LibraryEdit />} />
                            <Route path="media" element={<MediaPage />} />
                            <Route path="media/manage/:id" element={<p>Hello!</p>} />
                        </Route>
                </Routes>
                </HashRouter>
        </ThemeProvider>
    )
}

export default App;