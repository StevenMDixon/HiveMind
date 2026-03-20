import { Route, Routes, HashRouter } from 'react-router';
import './App.css';
import DashboardLayout from './Dashboard/layout';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

import ChannelPage from './Pages/Channels/Channels';
import LibaryPage from './Pages/Libraries/Library';
import CollectionPage from './Pages/Collections/Collections'
import MediaPage from './Pages/Media/MediaView'
import CollectionDetail from './Pages/Collections/CollectionDetail'
import Schedules from './Pages/Schedules/Schedules'
import ScheduleEdit from './Pages/Schedules/ScheduleEdit'
import ChannelEdit from './Pages/Channels/ChannelEdit';

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
                    <Route index element={<div>HiveMind Client</div>} />
                        <Route path="channels" element={<ChannelPage />} />
                        <Route path="channels/:id/" element={<ChannelEdit />} />
                        <Route path="schedules" element={<Schedules />} />
                        <Route path="schedules/:id" element={<ScheduleEdit />} />
                        <Route path="collections" element={<CollectionPage />} />
                        <Route path="collection/:id" element={<CollectionDetail />} />
                        <Route path="libraries" element={<LibaryPage />} />
                        <Route path="media" element={<MediaPage />} />
                        <Route path="media/manage/:id" element={<p>Hello!</p>} />
                    </Route>
            </Routes>
            </HashRouter>
        </ThemeProvider>
    )
}

export default App;