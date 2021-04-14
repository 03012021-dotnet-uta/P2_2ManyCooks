// import { HttpClient } from '@angular/common/http';
// import { async, TestBed } from '@angular/core/testing';
// import { Router, RouterModule } from '@angular/router';

// import { AuthService } from './auth.service';
// import { UrlService } from './url.service';
// import { UserService } from './user-service';

// class MockAuthService extends AuthService {
//   constructor() {
//     let r: Router;
//     let s: UserService;
//     let u: UrlService;
//     super(r, s, u);
//   }
// }

// describe('AuthService', () => {
//   let service: MockAuthService;


//   beforeEach(async(() => {
//     TestBed.configureTestingModule({
//       imports: [Router],
//       providers: [{
//         provide: AuthService,
//         useClass: MockAuthService
//       }]
//     })
//       .compileComponents();
//   }));


//   beforeEach(() => {
//     TestBed.configureTestingModule({});
//     service = TestBed.inject(AuthService);
//   });

//   it('should be created', () => {
//     expect(service).toBeTruthy();
//   });
// });
