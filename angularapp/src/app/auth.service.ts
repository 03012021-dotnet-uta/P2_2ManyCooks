import { Injectable } from '@angular/core';
// from: https://medium.com/swlh/using-auth0-to-secure-your-angular-application-and-access-your-asp-net-core-api-1947b9389f4f
import createAuth0Client from '@auth0/auth0-spa-js';
import Auth0Client from '@auth0/auth0-spa-js/dist/typings/Auth0Client';
import { from, of, Observable, BehaviorSubject, combineLatest, throwError, Subject } from 'rxjs';
import { tap, catchError, concatMap, shareReplay } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthModel } from './auth-model';
import { UserService } from './user-service';
import { UrlService } from './url.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public authModel$ = new Subject<AuthModel>();
  public isAdmin$ = new Subject<boolean>();
  public loading$ = new BehaviorSubject<boolean>(true);
  // getAuthModel() {
  //   this.userService.checkIfNewUser().subscribe((reply) => {
  //     if (reply.firstName == null && window.location.pathname != "/register") {
  //       console.log("firstname null");
  //       window.location.href = "register";
  //       // console.log(window.location.pathname);
  //       // console.log(window.location.href);
  //     }
  //     console.log("in auth service, getAuthModel()")
  //     console.log(reply);
  //     this.authModel = reply;
  //   }, () => { }, () => {
  //     // this.loading$.next(false);
  //     // this.notLoading = true;
  //   });
  // }

  getLoading$(): BehaviorSubject<boolean> {
    return this.loading$;
  }
  // Create an observable of Auth0 instance of client
  auth0Client$ = (from(
    // createAuth0Client({
    //   domain: 'dev-yktazjo3.us.auth0.com',
    //   client_id: 'DEJH5xmVrKbgDEwq5XmgjZqyftJLGrs5',
    //   redirect_uri: this.urlService.angularBaseUrl,
    //   audience: 'https://dev-yktazjo3.us.auth0.com/api/v2/'
    //   // audience: 'https://localhost:5001'
    // })
    createAuth0Client({
      domain: 'dev-yktazjo3.us.auth0.com',
      client_id: 'DEJH5xmVrKbgDEwq5XmgjZqyftJLGrs5',
      redirect_uri: this.urlService.angularBaseUrl,
      audience: 'https://inthekitchen/'
      // audience: 'https://localhost:5001'
    })
  ) as Observable<Auth0Client>).pipe(
    shareReplay(1), // Every subscription receives the same shared value
    catchError(err => throwError(err))
  );
  // Define observables for SDK methods that return promises by default
  // For each Auth0 SDK method, first ensure the client instance is ready
  // concatMap: Using the client instance, call SDK method; SDK returns a promise
  // from: Convert that resulting promise into an observable
  isAuthenticated$ = this.auth0Client$.pipe(
    concatMap((client: Auth0Client) => from(client.isAuthenticated())),
    tap(res => {
      this.loggedIn = res;
      this.notLoading = true;
      console.log("loggedIn is authenticated pipe" + this.loggedIn);
      console.log("notLoading " + this.notLoading);
      // this.loading$.next(false);
    })
  );
  handleRedirectCallback$ = this.auth0Client$.pipe(
    concatMap((client: Auth0Client) => from(client.handleRedirectCallback()))
  );
  // Create subject and public observable of user profile data
  private userProfileSubject$ = new BehaviorSubject<any>(null);
  userProfile$ = this.userProfileSubject$.asObservable();
  // Create a local property for login status
  loggedIn: boolean = null;
  notLoading: boolean = null;

  constructor(private router: Router, private userService: UserService, private urlService: UrlService) {

    // On initial load, check authentication state with authorization server
    // Set up local auth streams if user is already authenticated
    // this.loading = true;
    // console.log("in constructor loading: " + this.loading);
    this.localAuthSetup();
    // Handle redirect from Auth0 login
    this.handleAuthCallback();

    // this.isAuthenticated$.subscribe((reply) => {
    //   if (reply) {
    //     console.log("in subscribe isAuth");
    //     console.log(reply);
    //     this.userService.checkIfNewUser().then((reply) => {
    //       this.authModel$.next(reply);
    //       if (reply.firstName == null && window.location.pathname != "/register") {
    //         console.log("firstname null");
    //         this.router.navigate(["register"]);
    //         // console.log(window.location.pathname);
    //         // console.log(window.location.href);
    //       }

    //     });
    //   }
    // });
  }

  // initialize() {
  //   this.loading$.next(true);
  // }

  getTokenSilently$(options?): Observable<string> {
    return this.auth0Client$.pipe(
      concatMap((client: Auth0Client) => from(client.getTokenSilently(options)))
    );
  }

  // When calling, options can be passed if desired
  // https://auth0.github.io/auth0-spa-js/classes/auth0client.html#getuser
  getUser$(options?): Observable<any> {
    return this.auth0Client$.pipe(
      concatMap((client: Auth0Client) => from(client.getUser(options))),
      tap(user => {

        this.userProfileSubject$.next(user);
      })

    );
  }

  private localAuthSetup() {
    // this.loading$.next(true); 
    // This should only be called on app initialization
    // Set up local authentication streams
    // this.loading = true;
    const checkAuth$ = this.isAuthenticated$.pipe(
      concatMap((loggedIn: boolean) => {
        if (loggedIn) {
          console.log("loggedIn in checkauth pipe " + this.loggedIn);
          console.log("notLoading " + this.notLoading);
          // If authenticated, get user and set in app
          // NOTE: you could pass options here if needed
          return this.getUser$();
        }
        // If not authenticated, return stream that emits 'false'
        // this.loading = false;
        return of(loggedIn);
      })
    );

    const temp = this.getTokenSilently$();
    temp.subscribe(reply => {
      console.log("subscribed to token silent");
      console.log(reply);
      console.log("subscribed to token silent");
      this.tryRetrieveUser();
    })
    this.auth0Client$.subscribe(reply => {
      console.log("auth0Client$ subscription");
      console.log(reply);
      console.log("auth0Client$ subscription");
    })
    this.userProfile$.subscribe(reply => {
      console.log("this is user profile");
      console.log(reply);
      console.log("this is user profile");
    });
    this.userProfileSubject$.subscribe(reply => {
      console.log("userprofile");
      console.log(reply);

      console.log("user profile");

    });
    checkAuth$.subscribe((reply) => {
      console.log("in checkAuth$ subscription");
      console.log(reply);
      console.log("in checkAuth$ subscription");
      this.tryRetrieveUser();
      // this.loading = false;
      // this.getAuthModel();
    }, () => { }, () => {
      // this.loading$.next(false);
      // this.notLoading = true;
    });
  }
  tryRetrieveUser() {
    this.userService.checkIfNewUser().then(reply => {
      this.authModel$.next(reply);
      if (reply.firstName == null && window.location.pathname != "/register") {
        console.log("firstname null");
        this.router.navigate(["register"]);
        // console.log(window.location.pathname);
        // console.log(window.location.href);
      }
      else {
        this.isUserAdmin();
      }
    }).catch(err => {
      console.error(err);
      console.log("error getting user data" + err.message);
      this.isAdmin$.next(false);
    });
  }

  isUserAdmin() {
    this.userService.isUserAdmin().then(reply => {
      console.log("admin reply");
      console.log(reply);
      this.isAdmin$.next(true);
    }).catch(err => {
      console.log("error accessing admin");
      console.error(err);
      this.isAdmin$.next(false);
    });
  }

  login(redirectPath: string = '/') {
    // A desired redirect path can be passed to login method
    // (e.g., from a route guard)
    console.log("in login outside subscribe");
    // Ensure Auth0 client instance exists
    this.auth0Client$.subscribe((client: Auth0Client) => {
      console.log("in login - this.auth0Client$.subscribe(");
      console.log(client);
      // this.loading = true;
      // Call method to log in
      client.loginWithRedirect({
        redirect_uri: `${window.location.origin}`,
        appState: { target: redirectPath }
        // }).then(() => {
        //   this.loading = false;
        // }).catch(() => {
        //   this.loading = false;
      });
      // this.loading = false;
    });
  }

  private handleAuthCallback() {
    // Call when app reloads after user logs in with Auth0
    const params = window.location.search;
    if (params.includes('code=') && params.includes('state=')) {
      let targetRoute: string; // Path to redirect to after login processsed
      const authComplete$ = this.handleRedirectCallback$.pipe(
        // Have client, now call method to handle auth callback redirect
        tap(cbRes => {
          // Get and set target redirect route from callback results
          targetRoute = cbRes.appState && cbRes.appState.target ? cbRes.appState.target : '/';
        }),
        concatMap(() => {
          // Redirect callback complete; get user and login status
          // this.loading = false;
          return combineLatest([
            this.getUser$(),
            this.isAuthenticated$
          ]);
        })
      );
      // Subscribe to authentication completion observable
      // Response will be an array of user and login status
      authComplete$.subscribe(([user, loggedIn]) => {
        console.log("loggedIn in authcomplete " + this.loggedIn);
        console.log("notLoading " + this.notLoading);

        // Redirect to target route after callback processing
        this.router.navigate([targetRoute]);
        // this.loading = false;
      }, () => { }, () => {
        // this.notLoading = true;
        // this.loading$.next(false);
      });
    }
  }

  logout() {
    // Ensure Auth0 client instance exists
    this.auth0Client$.subscribe((client: Auth0Client) => {
      // Call method to log out
      client.logout({
        client_id: 'DEJH5xmVrKbgDEwq5XmgjZqyftJLGrs5',
        returnTo: `${window.location.origin}`
      });
      // this.loading = false;
    }, () => { }, () => {
      // this.notLoading = true;
      // this.loading$.next(false);
    });
  }

}
