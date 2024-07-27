import './App.css';
import {useState} from 'react';
import {Container, Grid, Typography, Box} from '@mui/material';
import SearchForm from './components/SearchForm';
import ResultsTable from './components/ResultsTable';
import DistinctSearchHistoryList from './components/DistinctSearchHistoryList';
import {getWebsiteRanks, WebsiteRankDto, SeoRequestDto, getHighestWebsiteRanksPerDay} from './services/Api';

const defaultSearchKeyword = 'land registry searches';
const defaultWebsiteUrl = 'www.infotrack.co.uk';
    
export default function App() {
    const [searchKeyword, setSearchKeyword] = useState<string>(defaultSearchKeyword);
    const [websiteUrl, setWebsiteUrl] = useState<string>(defaultWebsiteUrl);
    const [results, setResults] = useState<WebsiteRankDto[]>([]);
    const [highestDaily, setHighestDaily] = useState<WebsiteRankDto[]>([]);
    const [historyRefreshTrigger, setHistoryRefreshTrigger] = useState(0);

    const handleRefresh = () => {
        setHistoryRefreshTrigger(prev => prev + 1); // Trigger a change to refresh the component
    };
    
    const handleHighestWebsiteRanksPerDay = async () => {
        setHighestDaily(await getHighestWebsiteRanksPerDay({
            searchKeyword: searchKeyword,
            websiteUrl: websiteUrl,
        }));
    };
    
    const handleSearch = async (searchKeyword: string, websiteUrl: string) => {
        const seoRequestDto: SeoRequestDto = { searchKeyword, websiteUrl };
        setResults(await getWebsiteRanks(seoRequestDto));
        handleHighestWebsiteRanksPerDay();
        handleRefresh();
    };

    const handleHistorySelect = async (searchKeywordParam: string, websiteUrlParam: string) => {
        setSearchKeyword(searchKeywordParam);
        setWebsiteUrl(websiteUrlParam);
        if (searchKeywordParam !== searchKeyword || websiteUrlParam !== websiteUrl) {
            setResults([]);
        }
        setHighestDaily(await getHighestWebsiteRanksPerDay({
            searchKeyword: searchKeywordParam,
            websiteUrl: websiteUrlParam,
        }));
    };

    return (
        <>
            {/* Fixed list on the left side */}
            <Box
                sx={{
                    position: 'fixed',
                    left: 0,
                    top: 0,
                    bottom: 0,
                    width: '15%', // Adjust width as needed
                    overflowY: 'auto',
                    bgcolor: 'background.paper',
                    padding: 2,
                    boxShadow: 2, // Optional: adds a shadow for better visibility
                }}
            >
                <Typography variant="h5" gutterBottom>
                    History
                </Typography>
                <DistinctSearchHistoryList onSelect={handleHistorySelect} refreshTrigger={historyRefreshTrigger} />
            </Box>

            {/* Main content area */}
            <Container sx={{ marginLeft: '15%' }}> {/* Adjust marginLeft to match the width of the fixed list */}
                <Typography variant="h4" gutterBottom>
                    Google SERP Scraper
                </Typography>
                <Grid container spacing={0}>
                    <Grid item xs={12}>
                        <SearchForm
                            searchKeyword={searchKeyword}
                            websiteUrl={websiteUrl}
                            onSubmit={handleSearch}
                            onSearchKeywordChange={setSearchKeyword}
                            onWebsiteUrlChange={setWebsiteUrl}
                        />
                        {results.length > 0 && <div>
                            <Typography variant="h6" paddingTop={1} gutterBottom align="left">
                                Results
                            </Typography>
                            <ResultsTable results={results} />
                        </div>}
                        {highestDaily.length > 0 && <div>
                            <Typography variant="h6" paddingTop={2} gutterBottom align="left">
                                Top daily positions
                            </Typography>
                            <ResultsTable results={highestDaily} />
                        </div>}
                    </Grid>
                </Grid>
            </Container>
        </>
    );
};
