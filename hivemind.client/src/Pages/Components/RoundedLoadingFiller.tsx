import Skeleton from '@mui/material/Skeleton';
import Stack from '@mui/material/Stack';

interface RoundedLoadingFillerProps {
    size: number
}

const RoundedLoadingFiller = ({ size } : RoundedLoadingFillerProps) => (
    <Stack spacing={2} sx={{ m: 2 }}>
        {
            [...new Array(size)].map((_, i) => <Skeleton variant="rounded" height={60} key={i} />)
        }
    </Stack>
);

export default  RoundedLoadingFiller;