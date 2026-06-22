import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography'
interface HeaderProps {
    Title: string;
    children?: React.ReactNode | React.ReactNode[];
}

const Header = ({ Title, children }: HeaderProps) => {
    return (
        <AppBar position="static" color="secondary">
            <Toolbar>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    {Title}
                </Typography>
                {
                    children
                }
            </Toolbar>
        </AppBar>
    );
}

export default Header;