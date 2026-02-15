import { Route, Routes, HashRouter } from 'react-router';
import './App.css';
import DashboardLayout from './Dashboard/layout';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';

import ChannelPage from './Pages/Channels';
import LibaryPage from './Pages/Library';

const darkTheme = createTheme({
    palette: {
        mode: 'dark',
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
                        <Route path="schedules" element={<div>HiveMind Client: Schedules</div>} />
                        <Route path="collections" element={<div>HiveMind Client: Schedules</div>} />
                        <Route path="libraries" element={<LibaryPage />} />
                    </Route>
            </Routes>
            </HashRouter>
        </ThemeProvider>
    )
}

export default App;