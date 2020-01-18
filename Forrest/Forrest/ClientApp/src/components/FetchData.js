import React, { Component } from 'react';
import './Styles.css';

export class FetchData extends Component {
  static displayName = 'Forrest';

  constructor(props) {
    super(props);
      this.state = { placeDetails: [], search: "", loading: true };


      this.onChange = this.onChange.bind(this);
      this.onSubmit = this.onSubmit.bind(this);
  }

    onChange({ target }) {
        this.setState({
            [target.name]: target.value
        });
    }

    static renderPlaceDetails(placeDetails) {
        return (
            <div>
                {placeDetails.map(item =>
                    <div id="parent">
                        <div>{item.name}</div>
                        <div>{item.vicinity}</div>
                        <a href={item.website}>{item.website}</a>
                        <div>{item.opening_hours != null ? item.opening_hours.open_now ? 'Czy otwarte? Tak' : 'Czy otwarte? Nie' : null}</div>
                        <br />
                        <div className="weatherIcon"><img src={`https://www.accuweather.com/images/weathericons/${item.weatherForecast.weatherIcon}.svg`} width="60px" height="60px"></img></div>
                        <div className="temperature"><span> {item.weatherForecast.temperature.value}&#8451; </span></div>
                        <div className="weatherPhrase"><span>{item.weatherForecast.iconPhrase}</span></div>
                    </div>
                )}

            </div>
        );
    }

    render() {
        let contents = this.state.loading ? null : FetchData.renderPlaceDetails(this.state.placeDetails);
        return (
            <div>
                <form onSubmit={this.onSubmit}>
                    <input type="text" value={this.state.search} onChange={this.onChange} name="search" />
                    <button>Szukaj</button>
                    <br /> <br />
                </form>
                {contents}
            </div>
        );
    }

    onSubmit(e) {
        e.preventDefault();
        fetch('placedetails', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.state.search)
        })
            .then(response => response.json())
            .then(data => this.setState({ placeDetails: data, loading: false }))
    }
}
